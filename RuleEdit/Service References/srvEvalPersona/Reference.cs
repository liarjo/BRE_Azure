﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RuleEdit.srvEvalPersona {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="srvEvalPersona.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/EvalPersona", ReplyAction="http://tempuri.org/IService/EvalPersonaResponse")]
        RuleEdit.srvEvalPersona.EvalPersonaResponse EvalPersona(RuleEdit.srvEvalPersona.EvalPersonaRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/EvalPersona", ReplyAction="http://tempuri.org/IService/EvalPersonaResponse")]
        System.Threading.Tasks.Task<RuleEdit.srvEvalPersona.EvalPersonaResponse> EvalPersonaAsync(RuleEdit.srvEvalPersona.EvalPersonaRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EvalPersonaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://schemas.datacontract.org/2004/07/HelperBRE.SampleSubject", Order=0)]
        public HelperBRE.SampleSubject.Person Person;
        
        public EvalPersonaRequest() {
        }
        
        public EvalPersonaRequest(HelperBRE.SampleSubject.Person Person) {
            this.Person = Person;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EvalPersonaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://schemas.datacontract.org/2004/07/HelperBRE.SampleSubject", Order=0)]
        public HelperBRE.SampleSubject.Person Person;
        
        public EvalPersonaResponse() {
        }
        
        public EvalPersonaResponse(HelperBRE.SampleSubject.Person Person) {
            this.Person = Person;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : RuleEdit.srvEvalPersona.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<RuleEdit.srvEvalPersona.IService>, RuleEdit.srvEvalPersona.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RuleEdit.srvEvalPersona.EvalPersonaResponse RuleEdit.srvEvalPersona.IService.EvalPersona(RuleEdit.srvEvalPersona.EvalPersonaRequest request) {
            return base.Channel.EvalPersona(request);
        }
        
        public void EvalPersona(ref HelperBRE.SampleSubject.Person Person) {
            RuleEdit.srvEvalPersona.EvalPersonaRequest inValue = new RuleEdit.srvEvalPersona.EvalPersonaRequest();
            inValue.Person = Person;
            RuleEdit.srvEvalPersona.EvalPersonaResponse retVal = ((RuleEdit.srvEvalPersona.IService)(this)).EvalPersona(inValue);
            Person = retVal.Person;
        }
        
        public System.Threading.Tasks.Task<RuleEdit.srvEvalPersona.EvalPersonaResponse> EvalPersonaAsync(RuleEdit.srvEvalPersona.EvalPersonaRequest request) {
            return base.Channel.EvalPersonaAsync(request);
        }
    }
}
