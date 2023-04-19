# dotnet-configuration-test

A test to understand how dotnet ConfigurationBuilder merges multiple sources that have the same array property with complex objects. 

While testing, I discovered that by "inserting" an object in the array, it causes unexpected behavior by merging the inserted object and all items afterward with the existing "unshifted" configuration.

### Base Configuration
```json
{
  "KnownCertificates": [
    {
      "Subject": "FirstCert"
    },
    {
      "Subject": "SecondCert"
    }
  ]
}
```
 

### Cloud-specific configuration
```json
{
  "KnownCertificates": [
    {
      "Subject": "FirstCert"
    },
    {
      "Subject": "SecondCert",
      "AadAppId": "SecondCert-AadAppId"
    }
  ]
}
```
 

### Dynamic configuration
```json
{
  "KnownCertificates": [
    {
      "Subject": "FirstCert"
    },
    {
      "Subject": "NewCert" // Additional certificate
    },
    {
      "Subject": "SecondCert",
      "AadAppId": "SecondCert-AadAppId"
    }
  ]
}
```
 
## Output

### Output configuration from appsettings.json
```json
KnownCertificates:0:Subject = FirstCert

KnownCertificates:1:Subject = SecondCert
```


### Output configuration from appsettings.json and appsettings.public.json
```json
KnownCertificates:0:Subject = FirstCert

KnownCertificates:1:Subject = SecondCert
KnownCertificates:1:AadAppId = SecondCert-AadAppId
```

### Output configuration from appsettings.json, appsettings.public.json, and appsettings.cosmos.json
```json
KnownCertificates:0:Subject = FirstCert

KnownCertificates:1:Subject = NewCert
KnownCertificates:1:AadAppId = SecondCert-AadAppId // Inserted property

KnownCertificates:2:Subject = SecondCert
KnownCertificates:2:AadAppId = SecondCert-AadAppId
```