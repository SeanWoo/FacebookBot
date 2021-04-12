using Newtonsoft.Json;
using System;

namespace Twitter.Core.UserModels
{
    public partial class UserInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("profile_location")]
        public object ProfileLocation { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("entities")]
        public TemperaturesEntities Entities { get; set; }

        [JsonProperty("protected")]
        public bool Protected { get; set; }

        [JsonProperty("followers_count")]
        public long FollowersCount { get; set; }

        [JsonProperty("fast_followers_count")]
        public long FastFollowersCount { get; set; }

        [JsonProperty("normal_followers_count")]
        public long NormalFollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public long FriendsCount { get; set; }

        [JsonProperty("listed_count")]
        public long ListedCount { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("favourites_count")]
        public long FavouritesCount { get; set; }

        [JsonProperty("utc_offset")]
        public object UtcOffset { get; set; }

        [JsonProperty("time_zone")]
        public object TimeZone { get; set; }

        [JsonProperty("geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("statuses_count")]
        public long StatusesCount { get; set; }

        [JsonProperty("media_count")]
        public long MediaCount { get; set; }

        [JsonProperty("lang")]
        public object Lang { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty("is_translation_enabled")]
        public bool IsTranslationEnabled { get; set; }

        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonProperty("profile_background_image_url")]
        public Uri ProfileBackgroundImageUrl { get; set; }

        [JsonProperty("profile_background_image_url_https")]
        public Uri ProfileBackgroundImageUrlHttps { get; set; }

        [JsonProperty("profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }

        [JsonProperty("profile_image_url")]
        public Uri ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public Uri ProfileImageUrlHttps { get; set; }

        [JsonProperty("profile_banner_url")]
        public Uri ProfileBannerUrl { get; set; }

        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        [JsonProperty("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        [JsonProperty("profile_text_color")]
        public string ProfileTextColor { get; set; }

        [JsonProperty("profile_use_background_image")]
        public bool ProfileUseBackgroundImage { get; set; }

        [JsonProperty("has_extended_profile")]
        public bool HasExtendedProfile { get; set; }

        [JsonProperty("default_profile")]
        public bool DefaultProfile { get; set; }

        [JsonProperty("default_profile_image")]
        public bool DefaultProfileImage { get; set; }

        [JsonProperty("pinned_tweet_ids")]
        public double[] PinnedTweetIds { get; set; }

        [JsonProperty("pinned_tweet_ids_str")]
        public string[] PinnedTweetIdsStr { get; set; }

        [JsonProperty("has_custom_timelines")]
        public bool HasCustomTimelines { get; set; }

        [JsonProperty("can_media_tag")]
        public bool CanMediaTag { get; set; }

        [JsonProperty("followed_by")]
        public bool FollowedBy { get; set; }

        [JsonProperty("following")]
        public bool Following { get; set; }

        [JsonProperty("follow_request_sent")]
        public bool FollowRequestSent { get; set; }

        [JsonProperty("notifications")]
        public bool Notifications { get; set; }

        [JsonProperty("advertiser_account_type")]
        public string AdvertiserAccountType { get; set; }

        [JsonProperty("advertiser_account_service_levels")]
        public object[] AdvertiserAccountServiceLevels { get; set; }

        [JsonProperty("business_profile_state")]
        public string BusinessProfileState { get; set; }

        [JsonProperty("translator_type")]
        public string TranslatorType { get; set; }

        [JsonProperty("suspended")]
        public bool Suspended { get; set; }

        [JsonProperty("needs_phone_verification")]
        public bool NeedsPhoneVerification { get; set; }

        [JsonProperty("require_some_consent")]
        public bool RequireSomeConsent { get; set; }
    }

    public partial class TemperaturesEntities
    {
        [JsonProperty("url")]
        public EntitiesUrl Url { get; set; }

        [JsonProperty("description")]
        public Description Description { get; set; }
    }

    public partial class EntitiesUrl
    {
        [JsonProperty("urls")]
        public UrlElement[] Urls { get; set; }
    }
}
