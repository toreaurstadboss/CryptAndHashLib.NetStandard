# CryptAndHashLib.NetStandard

This is a simple package for cryption/encryption and hashing for use in .Net Standard.

## Encryption and decryption
The library supports one algorithm for encrypting and decrypting, the AES algorithm, taken from the 
.netstandard framework. Standard setup is used, with CBC as the mode with PKCS7 padding, 256 bits key and 16-bits initialization vector.
The key is retrieved from active configuration file and app setting "EncryptionKey". This library demands that you have this app setting.
The library does not support key vaults such as Azure Key vault. 

It is possible to specify initialization vector to the encryption, if not the auto-generated IV is selected.

## Hashing and verifying
Hashing and verifying is done using the Pbkdf2 key derivation. It is possible to specify pseudo random function as one of the KeyDerivationPrf functions and number of iterations.
Default if number of iterations and prf is not specified is 10,000 iterations and the HMACSHA1 prf is selected for compability with .NET Framework.
The verifying runs a SequenceEqual of hashed bytes input and generated hashed bytes of course. 

The hashing generates a 16 byte salt and 32 byte hash separated with a space character. To verify the hash, provide this hash with the salt to the verify method, do not 
split the string before as this is done internally in the verify method.

#### Generating new nuget package
Just run this dotnet command:

```bash
dotnet pack
```


### Use in production environment
This library could be used for small scale use in production environments. But using a key vault such as Azure Key Vault is strongly suggested.


## License 

The license of this library is MIT License. No warranties are given. You may freely redistribute and alter this library.


<hr />

Last update: 27.02.201

Tore Aurstad IT