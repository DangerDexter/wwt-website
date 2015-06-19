﻿//-----------------------------------------------------------------------
// <copyright file="IAdministrationService.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using WWTMVC5.Models;

namespace WWTMVC5.WebServices
{
    [ServiceContract]
    public interface IAdministrationService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We cannot use properties in a rest service call."), OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Offensive/Community")]
        Task<OffensiveEntityDetailsList> GetOffensiveCommunities();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We cannot use properties in a rest service call."), OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Offensive/Content")]
        Task<OffensiveEntityDetailsList> GetOffensiveContents();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OffensiveCommunityEntry/{id}", Method = "DELETE")]
        bool DeleteOffensiveCommunityEntry(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OffensiveContentEntry/{id}", Method = "DELETE")]
        bool DeleteOffensiveContentEntry(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OffensiveCommunity/{id}", Method = "DELETE")]
        bool DeleteOffensiveCommunity(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OffensiveContent/{id}", Method = "DELETE")]
        bool DeleteOffensiveContent(string id);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Community/{categoryID}")]
        Task<AdminEntityDetailsList> GetCommunities(string categoryId);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Content/{categoryID}")]
        Task<AdminEntityDetailsList> GetContents(string categoryId);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AdminUser")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We cannot use properties in a rest service call.")]
        Task<AdminUserDetailsList> GetUsers();

        //[OperationContract]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/User")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We cannot use properties in a rest service call.")]
        //AdminReportProfileDetailsList GetUsersForReport();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/FeaturedCommunity/{categoryID}", Method = "POST", RequestFormat = WebMessageFormat.Xml)]
        Task<bool> UpdateFeaturedCommunities(string categoryId, Stream featuredCommunities);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/FeaturedContent/{categoryID}", Method = "POST", RequestFormat = WebMessageFormat.Xml)]
        Task<bool> UpdateFeaturedContents(string categoryId, Stream featuredContents);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AdminUser", Method = "POST", RequestFormat = WebMessageFormat.Xml)]
        Task<bool> UpdateAdminUsers(Stream adminUsers);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AllCommunities/{categoryID}")]
        Task<AdminEntityDetailsList> GetAllCommunities(string categoryId);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AllContents/{categoryID}")]
        Task<AdminEntityDetailsList> GetAllContents(string categoryId);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UnDeleteCommunity/{id}", Method = "POST")]
        Task<bool> UnDeleteOffensiveCommunity(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UnDeleteContent/{id}", Method = "POST")]
        bool UnDeleteOffensiveContent(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PrivateCommunity/{id}", Method = "POST")]
        bool MarkAsPrivateCommunity(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PrivateContent/{id}", Method = "POST")]
        bool MarkAsPrivateContent(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PublicCommunity/{id}", Method = "POST")]
        bool MarkAsPublicCommunity(string id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PublicContent/{id}", Method = "POST")]
        bool MarkAsPublicContent(string id);
    }
}
