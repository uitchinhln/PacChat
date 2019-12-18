using PacChatServer.Cache.Core;
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
        LRUCache<Guid, ChatUserProfile> StoredProfiles { get; set; } = new LRUCache<Guid, ChatUserProfile>(1000, 20);
        ConcurrentDictionary<string, Guid> MappedEmail { get; set; } = new ConcurrentDictionary<string, Guid>();

        private ProfileCache()
        {

        }

        public ChatUserProfile GetUserProfile(Guid id)
        {
            if (StoredProfiles.Contains(id))
            {
                return StoredProfiles.Get(id);
            }

            ChatUserProfile profile = new UserProfileStore().LoadById(id);
            if (profile == null) return null;

            StoredProfiles.AddReplace(profile.ID, profile);
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
            StoredProfiles.AddReplace(profile.ID, profile);

            return profile.ID;
        }

        public static void StartService(bool forceStart = false)
        {
            try
            {
                if (Instance == null || forceStart)
                {
                    Instance = new ProfileCache(); 
                    PacChatServer.GetServer().Logger.Info("Profile Cache Service started successfully");
                }
            } catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }
        }

        public static ProfileCache Instance { get; private set; }
    }
}
