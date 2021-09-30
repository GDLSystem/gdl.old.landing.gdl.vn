using System;
using System.Collections.Generic;
using GdlCms.Web.Services.Gdll.Clients;
using GdlCms.Web.Services.Gdll.Core;
using GdlCms.Web.Services.Gdll.Entities;
using RestSharp;

namespace GdlCms.Web.Services.Gdll.Consumers
{
    public class GroupApiConsumer : BaseApiConsumer
    {
        public GroupApiConsumer(RestClient client) : base(client)
        {
        }

        public ApiResponse<CmsGdlLandingPageModel> GetCmsHomeData()
        {
            try
            {
                var restRequest = new RestRequest($"/app/cms-gdl/landing", Method.GET);

                //if (request.GroupCategoryType != null) restRequest.AddParameter("GroupCategoryType", (int)request.GroupCategoryType.Value);
            
                return HandleResponse<CmsGdlLandingPageModel>(Execute(restRequest));
            }
            catch (Exception e)
            {
                return new ApiResponse<CmsGdlLandingPageModel>();
            }
        }
        public ApiResponse<CmsGdlCommunityGroup> GetCmsGdlCommunityGroup(GroupCategoryType groupCategoryType)
        {
            try
            {
                var restRequest = new RestRequest($"/app/cms-gdl/communities", Method.GET);

                restRequest.AddParameter("groupCategoryType", (int)groupCategoryType);
            
                return HandleResponse<CmsGdlCommunityGroup>(Execute(restRequest));
            }
            catch (Exception e)
            {
                return new ApiResponse<CmsGdlCommunityGroup>();
            }
        }
    }
}