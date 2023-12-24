using Photon.Deterministic;

namespace Quantum 
{
    partial class RuntimePlayer 
    {
        public string NickName;
        public int WinCount;
        public AssetRefEntityPrototype CharacterPrototype;

        partial void SerializeUserData(BitStream stream)
        {
            stream.Serialize(ref NickName);
            stream.Serialize(ref WinCount);
            stream.Serialize(ref CharacterPrototype);
        }
    }
}
