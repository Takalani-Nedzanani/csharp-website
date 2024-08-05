using Firebase.Auth;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace firebase1.Repository.DataConnection
{
    public class FirebaseConnect : IDisposable
    {
        public IFirebaseClient firebaseClient;
        public IFirebaseAuthProvider authProvider;


        public FirebaseConnect()
        {
            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig()
            {
                AuthSecret = FirebaseConstants.AuthorizationSecret,
                BasePath = FirebaseConstants.databaseUrl
            };
            firebaseClient = new FireSharp.FirebaseClient(config);

            authProvider = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(FirebaseConstants.apiKey));


        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}