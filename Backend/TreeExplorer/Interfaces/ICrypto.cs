using System;

namespace TreeExplorer.Interfaces
{
    interface ICrypto
    {
        virtual string EncryptSha256(string stringToEncrypt) => throw new NotImplementedException();
    }
}
