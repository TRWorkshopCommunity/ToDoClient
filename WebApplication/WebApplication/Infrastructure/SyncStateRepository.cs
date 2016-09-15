using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Infrastructure
{
    public class SyncState
    {
        public bool Sync { get; set; }
        public int UserId { get; set; }
    }

    public sealed class SyncStateRepository
    {
        private static volatile SyncStateRepository instance;
        private static readonly object SyncRoot = new object();

        private SyncStateRepository()
        {
            SyncStateList = new List<SyncState>();
        }

        public static SyncStateRepository Instance
        {
            get
            {
                if (instance != null) return instance;
                lock (SyncRoot)
                {
                    if (instance == null)
                        instance = new SyncStateRepository();
                }

                return instance;
            }
        }

        private readonly List<SyncState> SyncStateList;

        public void Add(SyncState state)
        {
            SyncStateList.Add(state);
        }

        public bool IsSynchronized(int userId)
        {
            return SyncStateList.Any(e => e.UserId == userId);
        }
    }
}