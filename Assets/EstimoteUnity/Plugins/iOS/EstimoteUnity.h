#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <EstimoteSDK/EstimoteSDK.h>

extern void UnitySendMessage(const char *, const char *, const char *);

@interface EstimoteUnity : NSObject <ESTBeaconManagerDelegate>

@property (nonatomic) ESTBeaconManager *beaconManager;
@property (nonatomic) CLBeaconRegion *beaconRegion;
@property (nonatomic) BOOL hasInitialisedEstimote;

@end
