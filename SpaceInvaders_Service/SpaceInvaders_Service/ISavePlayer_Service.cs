using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SpaceInvaders_Service
{
    [ServiceContract]
    public interface ISavePlayer_Service
    {
        [OperationContract]
        bool InsertNewPlayer(Player player);

        [OperationContract]
        bool UpdatePlayer(Player player);

        [OperationContract]
        Player LoadPlayer(string username, string password);

        [OperationContract]
        bool IsPlayerExists(string username, string password);

        [OperationContract]
        bool IsUsernameExists(string username);

        [OperationContract]
        void RemoveFromDB(string username);
    }
}
