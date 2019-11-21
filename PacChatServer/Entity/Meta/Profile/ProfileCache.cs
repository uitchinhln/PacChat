using PacChatServer.IO.Entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entity.Meta.Profile
{
    public class ProfileCache
    {
        ConcurrentDictionary<Guid, ChatUserProfile> StoredProfiles { get; set; } = new ConcurrentDictionary<Guid, ChatUserProfile>();
        ConcurrentDictionary<string, Guid> MappedEmail { get; set; } = new ConcurrentDictionary<string, Guid>();

        private ProfileCache()
        {

        }

        public ChatUserProfile GetUserProfile(Guid id)
        {
            if (StoredProfiles.ContainsKey(id))
            {
                return StoredProfiles[id];
            }

            ChatUserProfile profile = new UserProfileStore().LoadById(id);
            if (profile == null) return null;

            StoredProfiles.TryAdd(profile.ID, profile);
            MappedEmail.TryAdd(profile.Email.ToLower(), profile.ID);

            return profile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>the respectively id. Empty Guid if email is not exist in db.</returns>
        public Guid ParseEmailToGuid(string email)
        {
            if (MappedEmail.ContainsKey(email.ToLower()))
            {
                return MappedEmail[email.ToLower()];
            }

            ChatUserProfile profile = new UserProfileStore().LoadByEmail(email);

            if (profile == null) return Guid.Empty;

            MappedEmail.TryAdd(email.ToLower(), profile.ID);
            StoredProfiles.TryAdd(profile.ID, profile);

            return profile.ID;
        }

        public static void StartService(bool forceStart = false)
        {
            if (Instance == null || forceStart)
            {
                Instance = new ProfileCache();
            }
        }

        public static ProfileCache Instance { get; private set; }
    }
}
