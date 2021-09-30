using System;
using System.Collections.Generic;

namespace GdlCms.Web.Services.Gdll.Entities
{
    public class CmsGdlLandingPageModel
    {
        public int GroupCount { get; set; }
        public int PageCount { get; set; }
        public int PartnerCount { get; set; }
        public List<BrandingCommunity> BrandingCommunities { get; set; }
        public List<CmsGdlFeatureCommunityGroup> FeatureCommunities { get; set; }
        public CmsGdlLandingPageModel()
        {
            BrandingCommunities = new List<BrandingCommunity>();
            FeatureCommunities = new List<CmsGdlFeatureCommunityGroup>();
        }
        public class BrandingCommunity
        {
            public string Name { get; set; }
            public int MemberCount { get; set; }
            public decimal GrowthPercentagePerMonth { get; set; }
            public int TotalReactionsPerMonth { get; set; }
            public string Url { get; set; }
        }
    }
    public enum GroupCategoryType
    {
        Unknown = 1,
        Architecture = 10,
        Beauty = 20,
        FnB = 30,
        TravelAndSport = 40,
        General = 90,
        Dalat = 100,
        HappyDay = 900
    }
    public class CmsGdlFeatureCommunityGroup
    {
        public string CategoryName { get; set; }
        public GroupCategoryType GroupCategoryType { get; set; }
    }
    public class CmsGdlCommunityGroup
    {
        public List<Item> Level1 { get; set; }
        public List<Item> Level2 { get; set; }
        public List<Item> Level3 { get; set; }
        public List<CmsGdlFeatureCommunityGroup> OtherCommunities { get; set; }
        public CmsGdlCommunityGroup()
        {
            OtherCommunities = new List<CmsGdlFeatureCommunityGroup>();
            Level1 = new List<Item>();
            Level2 = new List<Item>();
            Level3 = new List<Item>();
        }
        public class Item
        {
            public string Title { get; set; }
            public string ShortDescription { get; set; }
            public int MemberCount { get; set; }
            public decimal GrowthPercentagePerMonth { get; set; }
            public int TotalReactionsPerMonth { get; set; }
            public string Url { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}