using System;

namespace TreeExplorer.VirtualClasses
{
    public class VirtualCrypto
    {
        public virtual string EncryptSha256(string stringToEncrypt) => throw new NotImplementedException();
    }
}
