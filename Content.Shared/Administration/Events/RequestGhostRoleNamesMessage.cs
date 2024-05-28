using Lidgren.Network;
using Robust.Shared.Network;
using Robust.Shared.Serialization;

namespace Content.Shared.Administration.Events
{
    public class MsgRequestGhostRoleNames : NetMessage
    {
        public override MsgGroups MsgGroup => MsgGroups.Command;
        public NetUserId UserId { get; set; } // Add this line to carry the user ID

        public override void ReadFromBuffer(NetIncomingMessage buffer, IRobustSerializer serializer)
        {
            UserId = new NetUserId(buffer.ReadGuid()); // Corrected reading method
        }

        public override void WriteToBuffer(NetOutgoingMessage buffer, IRobustSerializer serializer)
        {
            buffer.Write(UserId.UserId); // Corrected writing method
        }
    }

    public sealed class MsgReceiveGhostRoleNames : NetMessage
    {
        public override MsgGroups MsgGroup => MsgGroups.Command;

        public List<string> RoleNames = new();

        public override void ReadFromBuffer(NetIncomingMessage buffer, IRobustSerializer serializer)
        {
            var count = buffer.ReadInt32(); // Corrected to ReadInt32 for consistency
            RoleNames.Clear(); // Ensure list is cleared before reading
            for (var i = 0; i < count; i++)
            {
                RoleNames.Add(buffer.ReadString());
            }
        }

        public override void WriteToBuffer(NetOutgoingMessage buffer, IRobustSerializer serializer)
        {
            buffer.Write(RoleNames.Count);
            foreach (var roleName in RoleNames)
            {
                buffer.Write(roleName);
            }
        }
    }
}
