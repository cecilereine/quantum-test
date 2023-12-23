using Photon.Deterministic;

namespace Quantum 
{
    partial class RuntimePlayer 
    {
        public string NickName;
        public AssetRefEntityPrototype CharacterPrototype;

        partial void SerializeUserData(BitStream stream)
        {
            stream.Serialize(ref NickName);
            stream.Serialize(ref CharacterPrototype);
        }
    }
}
