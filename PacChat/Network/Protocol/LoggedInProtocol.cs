using PacChat.Network.Packets.AfterLoginRequest;
using PacChat.Network.Packets.AfterLoginRequest.Message;
using PacChat.Network.Packets.AfterLoginRequest.Notification;
using PacChat.Network.Packets.AfterLoginRequest.Profile;
using PacChat.Network.Packets.AfterLoginRequest.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Protocol
{
    public class LoggedInProtocol : PacChatProtocol
    {
        public LoggedInProtocol() : base("LoggedIn Protocol")
        {
            Inbound(0x20, new GetSelfIDResult());
            Outbound(0x20, new GetSelfID());

            Inbound(0x00, new GetFriendIDsResult());
            Outbound(0x00, new GetFriendIDs());

            Inbound(0x01, new GetShortInfoResult());
            Outbound(0x01, new GetShortInfo());

            Inbound(0x04, new ReceiveMessage());

            Outbound(0x05, new SendMessage());

            Inbound(0x02, new SingleConversationFrUserIDResult());
            Outbound(0x02, new SingleConversationFrUserID());

            Inbound(0x16, new GetMessageFromConversationResult());
            Outbound(0x16, new GetMessageFromConversation());

            Inbound(0x06, new ConversationFromIDResult());
            Outbound(0x06, new ConversationFromID());

            Inbound(0x17, new GetConversationShortInfoResult());
            Outbound(0x17, new GetConversationShortInfo());

            Inbound(0x18, new RecentConversationsResult());
            Outbound(0x18, new RecentConversations());

            // Notifications
            Inbound(0x07, new UserOnlineReceive());
            Inbound(0x08, new UserOfflineReceive());

            Inbound(0x10, new FriendRequestReceive());
            Outbound(0x11, new FriendRequest());
            Inbound(0x12, new FriendRequestResult());
            Outbound(0x13, new ResponseFriendRequest());
            Inbound(0x14, new FinalizeAcceptedFriendRequestReceive());

            Inbound(0x15, new GetNotificationsResult());
            Outbound(0x15, new GetNotifications());

            // Search
            Inbound(0x03, new SearchUserResult());
            Outbound(0x03, new SearchUser());

            // Profile
            Inbound(0x09, new GetDisplayedProfileResult());
            Outbound(0x09, new GetDisplayedProfile());
        }
    }
}
