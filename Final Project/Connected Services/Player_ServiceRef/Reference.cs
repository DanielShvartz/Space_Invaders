﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 15.0.28307.1000
// 
namespace Final_Project.Player_ServiceRef {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/SpaceInvaders_Service")]
    public partial class Player : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int Bullet_LevelField;
        
        private int CoinsField;
        
        private int Current_LevelField;
        
        private int HPField;
        
        private string PasswordField;
        
        private int Shield1_HPField;
        
        private int Shield1_ImageField;
        
        private int Shield2_HPField;
        
        private int Shield2_ImageField;
        
        private int Shield3_HPField;
        
        private int Shield3_ImageField;
        
        private int SpaceShip_LevelField;
        
        private string UsernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Bullet_Level {
            get {
                return this.Bullet_LevelField;
            }
            set {
                if ((this.Bullet_LevelField.Equals(value) != true)) {
                    this.Bullet_LevelField = value;
                    this.RaisePropertyChanged("Bullet_Level");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Coins {
            get {
                return this.CoinsField;
            }
            set {
                if ((this.CoinsField.Equals(value) != true)) {
                    this.CoinsField = value;
                    this.RaisePropertyChanged("Coins");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Current_Level {
            get {
                return this.Current_LevelField;
            }
            set {
                if ((this.Current_LevelField.Equals(value) != true)) {
                    this.Current_LevelField = value;
                    this.RaisePropertyChanged("Current_Level");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HP {
            get {
                return this.HPField;
            }
            set {
                if ((this.HPField.Equals(value) != true)) {
                    this.HPField = value;
                    this.RaisePropertyChanged("HP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Shield1_HP {
            get {
                return this.Shield1_HPField;
            }
            set {
                if ((this.Shield1_HPField.Equals(value) != true)) {
                    this.Shield1_HPField = value;
                    this.RaisePropertyChanged("Shield1_HP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Shield1_Image {
            get {
                return this.Shield1_ImageField;
            }
            set {
                if ((this.Shield1_ImageField.Equals(value) != true)) {
                    this.Shield1_ImageField = value;
                    this.RaisePropertyChanged("Shield1_Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Shield2_HP {
            get {
                return this.Shield2_HPField;
            }
            set {
                if ((this.Shield2_HPField.Equals(value) != true)) {
                    this.Shield2_HPField = value;
                    this.RaisePropertyChanged("Shield2_HP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Shield2_Image {
            get {
                return this.Shield2_ImageField;
            }
            set {
                if ((this.Shield2_ImageField.Equals(value) != true)) {
                    this.Shield2_ImageField = value;
                    this.RaisePropertyChanged("Shield2_Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Shield3_HP {
            get {
                return this.Shield3_HPField;
            }
            set {
                if ((this.Shield3_HPField.Equals(value) != true)) {
                    this.Shield3_HPField = value;
                    this.RaisePropertyChanged("Shield3_HP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Shield3_Image {
            get {
                return this.Shield3_ImageField;
            }
            set {
                if ((this.Shield3_ImageField.Equals(value) != true)) {
                    this.Shield3_ImageField = value;
                    this.RaisePropertyChanged("Shield3_Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SpaceShip_Level {
            get {
                return this.SpaceShip_LevelField;
            }
            set {
                if ((this.SpaceShip_LevelField.Equals(value) != true)) {
                    this.SpaceShip_LevelField = value;
                    this.RaisePropertyChanged("SpaceShip_Level");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Player_ServiceRef.ISavePlayer_Service")]
    public interface ISavePlayer_Service {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISavePlayer_Service/SavePlayer", ReplyAction="http://tempuri.org/ISavePlayer_Service/SavePlayerResponse")]
        System.Threading.Tasks.Task<bool> SavePlayerAsync(Final_Project.Player_ServiceRef.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISavePlayer_Service/UpdatePlayer", ReplyAction="http://tempuri.org/ISavePlayer_Service/UpdatePlayerResponse")]
        System.Threading.Tasks.Task<bool> UpdatePlayerAsync(Final_Project.Player_ServiceRef.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISavePlayer_Service/LoadPlayer", ReplyAction="http://tempuri.org/ISavePlayer_Service/LoadPlayerResponse")]
        System.Threading.Tasks.Task<Final_Project.Player_ServiceRef.Player> LoadPlayerAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISavePlayer_Service/IsPlayerExists", ReplyAction="http://tempuri.org/ISavePlayer_Service/IsPlayerExistsResponse")]
        System.Threading.Tasks.Task<bool> IsPlayerExistsAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISavePlayer_ServiceChannel : Final_Project.Player_ServiceRef.ISavePlayer_Service, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SavePlayer_ServiceClient : System.ServiceModel.ClientBase<Final_Project.Player_ServiceRef.ISavePlayer_Service>, Final_Project.Player_ServiceRef.ISavePlayer_Service {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SavePlayer_ServiceClient() : 
                base(SavePlayer_ServiceClient.GetDefaultBinding(), SavePlayer_ServiceClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ISavePlayer_Service.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SavePlayer_ServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(SavePlayer_ServiceClient.GetBindingForEndpoint(endpointConfiguration), SavePlayer_ServiceClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SavePlayer_ServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SavePlayer_ServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SavePlayer_ServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SavePlayer_ServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SavePlayer_ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<bool> SavePlayerAsync(Final_Project.Player_ServiceRef.Player player) {
            return base.Channel.SavePlayerAsync(player);
        }
        
        public System.Threading.Tasks.Task<bool> UpdatePlayerAsync(Final_Project.Player_ServiceRef.Player player) {
            return base.Channel.UpdatePlayerAsync(player);
        }
        
        public System.Threading.Tasks.Task<Final_Project.Player_ServiceRef.Player> LoadPlayerAsync(string username, string password) {
            return base.Channel.LoadPlayerAsync(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> IsPlayerExistsAsync(string username, string password) {
            return base.Channel.IsPlayerExistsAsync(username, password);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISavePlayer_Service)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISavePlayer_Service)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/Design_Time_Addresses/SpaceInvaders_Service/SavePlayer_Serv" +
                        "ice/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return SavePlayer_ServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ISavePlayer_Service);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return SavePlayer_ServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ISavePlayer_Service);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_ISavePlayer_Service,
        }
    }
}