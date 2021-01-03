#import <Foundation/Foundation.h>
#import <Pushe/Pushe-Swift.h>

char* cStringCopy(const char*);

extern "C" {
    void xcodeLog(const char *message) {
        NSString *string = [[NSString alloc] initWithUTF8String:message];
        NSLog(@"++++++++++");
        NSLog(@"🎄 -> %@", string);
        NSLog(@"++++++++++");
    }

    void initialize() {
        [PusheClient.shared initialize];
    }

    bool isRegistered() {
        return [PusheClient.shared isRegistered];
    }

    char * getDeviceId() {
        NSString *result = [PusheClient.shared getDeviceId];
        return cStringCopy([result UTF8String]);
    }

    char * getAdvertisingId() {
        NSString *result = [PusheClient.shared getAdvertisingId];
        return cStringCopy([result UTF8String]);
    }

    void subscribeTo(const char *topic) {
        NSString *string = [[NSString alloc] initWithUTF8String:topic];
        [PusheClient.shared subscribe:string];
    }

    void unsubscribeFrom(const char *topic) {
        NSString *string = [[NSString alloc] initWithUTF8String:topic];
        [PusheClient.shared unsubscribe:string];
    }

    char * getSubscribedTopics() {
        NSArray *array = [[NSArray alloc] initWithArray:[PusheClient.shared getSubscribedTopics]];
        NSString *json = NULL;
        NSError *error = NULL;
        NSData *data = [NSJSONSerialization dataWithJSONObject:array options:NULL error:&error];
        json = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];

        if (error != NULL) {
            return NULL;
        } else {
            return cStringCopy([json UTF8String]);
        }
    }

    void addTags(const char *tags) {
        NSString *string = [[NSString alloc] initWithUTF8String:tags];
        NSArray *keyValues = [string componentsSeparatedByString:@","];
        NSMutableDictionary<NSString *, NSString*> *dict = [[NSMutableDictionary alloc]initWithCapacity:keyValues.count];
        for (int i = 0; i < keyValues.count; i++) {
            NSArray *temp = [keyValues[i] componentsSeparatedByString:@":"];
            NSString *key = temp[0];
            NSString *value = temp[1];
            [dict setObject:value forKey:key];
        }

        [PusheClient.shared addTags:dict];
    }

    void removeTags(const char * keys) {
        xcodeLog(keys);
        NSString *string = [[NSString alloc] initWithUTF8String:keys];
        NSArray *keysArray = [string componentsSeparatedByString:@","];
        [PusheClient.shared removeTags:keysArray];
    }

    char * getSubscribedTags() {
        NSDictionary<NSString *, NSString*> *tags = [[NSDictionary alloc] initWithDictionary:[PusheClient.shared getSubscribedTags]];
        NSString *json = NULL;
        NSError *error = NULL;
        NSData *data = [NSJSONSerialization dataWithJSONObject:tags options:NULL error:&error];
        json = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        
        if (error != NULL) {
            return NULL;
        } else {
            return cStringCopy([json UTF8String]);
        }
    }

    void sendEvent(const char * eventName) {
        if (eventName == NULL) {
            return;
        }
        
        NSString *string = [[NSString alloc] initWithUTF8String:eventName];
        [PusheClient.shared sendEventWithName:string];
    }

    void setCustomId(const char * customId) {
        NSString *string;
        if (customId == NULL) {
            string = NULL;
        } else {
            string = [[NSString alloc] initWithUTF8String:customId];
        }
        
        [PusheClient.shared setCustomId:string];
    }

    char * getCustomId() {
        NSString *result = [PusheClient.shared getCustomId];
        return cStringCopy([result UTF8String]);
    }

    bool setUserEmail(const char * email) {
        NSString *string;
        if (email == NULL) {
            string = NULL;
        } else {
            string = [[NSString alloc] initWithUTF8String:email];
        }
        
        return [PusheClient.shared setUserEmail:string];
    }

    char * getUserEmail() {
        NSString *result = [PusheClient.shared getUserEmail];
        return cStringCopy([result UTF8String]);
    }

    bool setUserPhoneNumber(const char * phoneNumber) {
        NSString *string;
        if (phoneNumber == NULL) {
            string = NULL;
        } else {
            string = [[NSString alloc] initWithUTF8String:phoneNumber];
        }
        
        return [PusheClient.shared setUserPhoneNumber:string];
    }

    char * getUserPhoneNumber() {
        NSString *result = [PusheClient.shared getUserPhoneNumber];
        return cStringCopy([result UTF8String]);
    }
}

char* cStringCopy(const char* string) {
    if (string == NULL)
        return NULL;

    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);

    return res;
}
