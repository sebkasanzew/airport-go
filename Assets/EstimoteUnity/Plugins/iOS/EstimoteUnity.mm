#import "EstimoteUnity.h"

@implementation EstimoteUnity

- (void) initEstimote: (NSString *)beaconUUIDString
{
    self.beaconManager = [ESTBeaconManager new];
    self.beaconManager.delegate = self;
    self.beaconRegion = [[CLBeaconRegion alloc] initWithProximityUUID:[[NSUUID alloc] initWithUUIDString:beaconUUIDString] identifier:@"ranged beacons"];
    [self.beaconManager requestAlwaysAuthorization];
    self.hasInitialisedEstimote = YES;
}

- (void) startScanning
{
    NSLog(@"[EstimoteUnity] startScanning");
    [self.beaconManager startRangingBeaconsInRegion:self.beaconRegion];
}

- (void) stopScanning
{
    NSLog(@"[EstimoteUnity] stopScanning");
    [self.beaconManager stopRangingBeaconsInAllRegions];
}

- (void)beaconManager:(id)manager didRangeBeacons:(NSArray<CLBeacon *> *)beacons inRegion:(CLBeaconRegion *)region
{
    if (beacons.count == 0) {
        NSLog(@"No beacons found");
    }
    
    NSMutableArray* beaconsArray = [[NSMutableArray alloc] init];
    
    for(CLBeacon *beacon in beacons) {
        // Create the beacon dictionary which will convert into JSON
        NSMutableDictionary* beaconDictionary = [[NSMutableDictionary alloc] init];
        [beaconDictionary setObject:beacon.proximityUUID.UUIDString forKey:@"UUID"];
        [beaconDictionary setObject:beacon.major forKey:@"Major"];
        [beaconDictionary setObject:beacon.minor forKey:@"Minor"];
        [beaconDictionary setObject:[NSNumber numberWithInt:beacon.proximity] forKey:@"BeaconRange"];
        [beaconDictionary setObject:[NSNumber numberWithInteger:beacon.rssi] forKey:@"RSSI"];
        [beaconDictionary setObject:[NSNumber numberWithDouble:beacon.accuracy] forKey:@"Accuracy"];
        
        // Add the beacon dictionary into the beacons array
        [beaconsArray addObject: beaconDictionary];
    }
    
    NSData* beaconsData = [NSJSONSerialization dataWithJSONObject:beaconsArray options:NSJSONWritingPrettyPrinted error:nil];
    NSString* json = [[NSString alloc] initWithData:beaconsData encoding:NSUTF8StringEncoding];
    UnitySendMessage("EstimoteUnity","DidRangeBeacons", [[NSString stringWithString:json] cStringUsingEncoding:NSUTF8StringEncoding]);
}

@end

extern "C" {
    EstimoteUnity *sInstance;
    
    void StartEstimoteScanning(char * beaconUUID)
    {
        if(sInstance == nil) {
            // Create our instance
            sInstance = [EstimoteUnity alloc];
            
            // Initialise Estimote
            [sInstance initEstimote:[NSString stringWithUTF8String:beaconUUID]];
            [sInstance startScanning];
        }
        else {
            if(sInstance.hasInitialisedEstimote == NO) {
                [sInstance initEstimote:[NSString stringWithUTF8String:beaconUUID]];
            }
            // Start scanning as we already are initialised
            [sInstance startScanning];
        }
    }
    
    void StopEstimoteScanning()
    {
        if(sInstance != nil) {
            [sInstance stopScanning];
        }
    }
    
    int CheckDeviceSupportsBeacons()
    {
        // Check to see if the device supports beacons
        if(![CLLocationManager isRangingAvailable]) {
            return 0;
        }
        
        return 1;
    }
}
