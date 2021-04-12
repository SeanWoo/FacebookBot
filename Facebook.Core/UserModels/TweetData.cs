using Newtonsoft.Json;
using System;

namespace Twitter.Core.UserModels
{

    /*public partial class TweetData
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("entities")]
        public WelcomeEntities Entities { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public WelcomeUser User { get; set; }

        [JsonProperty("geo")]
        public Coordinates Geo { get; set; }

        [JsonProperty("coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("place")]
        public Place Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty("conversation_id")]
        public double ConversationId { get; set; }

        [JsonProperty("conversation_id_str")]
        public string ConversationIdStr { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("possibly_sensitive", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PossiblySensitive { get; set; }

        [JsonProperty("possibly_sensitive_editable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PossiblySensitiveEditable { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("supplemental_language")]
        public object SupplementalLanguage { get; set; }

        [JsonProperty("self_thread", NullValueHandling = NullValueHandling.Ignore)]
        public SelfThread SelfThread { get; set; }

        [JsonProperty("extended_entities", NullValueHandling = NullValueHandling.Ignore)]
        public WelcomeExtendedEntities ExtendedEntities { get; set; }

        [JsonProperty("retweeted_status", NullValueHandling = NullValueHandling.Ignore)]
        public RetweetedStatus RetweetedStatus { get; set; }

        [JsonProperty("quoted_status_id", NullValueHandling = NullValueHandling.Ignore)]
        public double? QuotedStatusId { get; set; }

        [JsonProperty("quoted_status_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string QuotedStatusIdStr { get; set; }

        [JsonProperty("quoted_status", NullValueHandling = NullValueHandling.Ignore)]
        public QuotedStatus QuotedStatus { get; set; }
    }

    public partial class WelcomeEntities
    {
        [JsonProperty("hashtags")]
        public Hashtag[] Hashtags { get; set; }

        [JsonProperty("symbols")]
        public object[] Symbols { get; set; }

        [JsonProperty("user_mentions")]
        public UserMention[] UserMentions { get; set; }

        [JsonProperty("urls")]
        public PurpleUrl[] Urls { get; set; }

        [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleMedia[] Media { get; set; }
    }

    public partial class Hashtag
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class PurpleMedia
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("original_info")]
        public OriginalInfo OriginalInfo { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty("source_status_id")]
        public double SourceStatusId { get; set; }

        [JsonProperty("source_status_id_str")]
        public string SourceStatusIdStr { get; set; }

        [JsonProperty("source_user_id")]
        public double SourceUserId { get; set; }

        [JsonProperty("source_user_id_str")]
        public string SourceUserIdStr { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }
    }

    public partial class Features
    {
        [JsonProperty("orig", NullValueHandling = NullValueHandling.Ignore)]
        public OrigClass Orig { get; set; }

        [JsonProperty("large", NullValueHandling = NullValueHandling.Ignore)]
        public OrigClass Large { get; set; }

        [JsonProperty("medium", NullValueHandling = NullValueHandling.Ignore)]
        public OrigClass Medium { get; set; }

        [JsonProperty("small", NullValueHandling = NullValueHandling.Ignore)]
        public OrigClass Small { get; set; }
    }

    public partial class OrigClass
    {
        [JsonProperty("faces")]
        public FocusRect[] Faces { get; set; }
    }

    public partial class FocusRect
    {
        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        [JsonProperty("h")]
        public long H { get; set; }

        [JsonProperty("w")]
        public long W { get; set; }
    }

    public partial class OriginalInfo
    {
        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("focus_rects", NullValueHandling = NullValueHandling.Ignore)]
        public FocusRect[] FocusRects { get; set; }
    }

    public partial class Sizes
    {
        [JsonProperty("thumb")]
        public ThumbClass Thumb { get; set; }

        [JsonProperty("large")]
        public ThumbClass Large { get; set; }

        [JsonProperty("medium")]
        public ThumbClass Medium { get; set; }

        [JsonProperty("small")]
        public ThumbClass Small { get; set; }
    }

    public partial class ThumbClass
    {
        [JsonProperty("w")]
        public long W { get; set; }

        [JsonProperty("h")]
        public long H { get; set; }

        [JsonProperty("resize")]
        public string Resize { get; set; }
    }

    public partial class PurpleUrl
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class UserMention
    {
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class WelcomeExtendedEntities
    {
        [JsonProperty("media")]
        public FluffyMedia[] Media { get; set; }
    }

    public partial class FluffyMedia
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("original_info")]
        public OriginalInfo OriginalInfo { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty("source_status_id")]
        public double SourceStatusId { get; set; }

        [JsonProperty("source_status_id_str")]
        public string SourceStatusIdStr { get; set; }

        [JsonProperty("source_user_id")]
        public double SourceUserId { get; set; }

        [JsonProperty("source_user_id_str")]
        public string SourceUserIdStr { get; set; }

        [JsonProperty("video_info", NullValueHandling = NullValueHandling.Ignore)]
        public VideoInfo VideoInfo { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }

        [JsonProperty("media_key")]
        public string MediaKey { get; set; }

        [JsonProperty("additional_media_info", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleAdditionalMediaInfo AdditionalMediaInfo { get; set; }
    }

    public partial class PurpleAdditionalMediaInfo
    {
        [JsonProperty("monetizable")]
        public bool Monetizable { get; set; }

        [JsonProperty("source_user")]
        public SourceUser SourceUser { get; set; }
    }

    public partial class SourceUser
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("entities")]
        public SourceUserEntities Entities { get; set; }

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

        [JsonProperty("contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty("is_translation_enabled")]
        public bool IsTranslationEnabled { get; set; }

        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonProperty("profile_background_image_url")]
        public object ProfileBackgroundImageUrl { get; set; }

        [JsonProperty("profile_background_image_url_https")]
        public object ProfileBackgroundImageUrlHttps { get; set; }

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
        public string[] AdvertiserAccountServiceLevels { get; set; }

        [JsonProperty("business_profile_state")]
        public string BusinessProfileState { get; set; }

        [JsonProperty("translator_type")]
        public string TranslatorType { get; set; }

        [JsonProperty("require_some_consent")]
        public bool RequireSomeConsent { get; set; }
    }

    public partial class SourceUserEntities
    {
        [JsonProperty("url")]
        public FluffyUrl Url { get; set; }

        [JsonProperty("description")]
        public PurpleDescription Description { get; set; }
    }

    public partial class PurpleDescription
    {
        [JsonProperty("urls")]
        public object[] Urls { get; set; }
    }

    public partial class FluffyUrl
    {
        [JsonProperty("urls")]
        public UrlUrl[] Urls { get; set; }
    }

    public partial class UrlUrl
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class VideoInfo
    {
        [JsonProperty("aspect_ratio")]
        public long[] AspectRatio { get; set; }

        [JsonProperty("duration_millis")]
        public long DurationMillis { get; set; }

        [JsonProperty("variants")]
        public Variant[] Variants { get; set; }
    }

    public partial class Variant
    {
        [JsonProperty("bitrate", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bitrate { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Place
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("place_type")]
        public string PlaceType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("contained_within")]
        public object[] ContainedWithin { get; set; }

        [JsonProperty("bounding_box")]
        public BoundingBox BoundingBox { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
    }

    public partial class BoundingBox
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[][][] Coordinates { get; set; }
    }

    public partial class QuotedStatus
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("entities")]
        public QuotedStatusEntities Entities { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public QuotedStatusUser User { get; set; }

        [JsonProperty("geo")]
        public object Geo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty("conversation_id")]
        public double ConversationId { get; set; }

        [JsonProperty("conversation_id_str")]
        public string ConversationIdStr { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("supplemental_language")]
        public object SupplementalLanguage { get; set; }
    }

    public partial class QuotedStatusEntities
    {
        [JsonProperty("hashtags")]
        public object[] Hashtags { get; set; }

        [JsonProperty("symbols")]
        public object[] Symbols { get; set; }

        [JsonProperty("user_mentions")]
        public object[] UserMentions { get; set; }

        [JsonProperty("urls")]
        public object[] Urls { get; set; }
    }

    public partial class QuotedStatusUser
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

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("entities")]
        public SourceUserEntities Entities { get; set; }

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

        [JsonProperty("require_some_consent")]
        public bool RequireSomeConsent { get; set; }
    }

    public partial class RetweetedStatus
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("entities")]
        public RetweetedStatusEntities Entities { get; set; }

        [JsonProperty("extended_entities", NullValueHandling = NullValueHandling.Ignore)]
        public RetweetedStatusExtendedEntities ExtendedEntities { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public double? InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public double? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public RetweetedStatusUser User { get; set; }

        [JsonProperty("geo")]
        public object Geo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty("conversation_id")]
        public double ConversationId { get; set; }

        [JsonProperty("conversation_id_str")]
        public string ConversationIdStr { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("possibly_sensitive", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PossiblySensitive { get; set; }

        [JsonProperty("possibly_sensitive_editable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PossiblySensitiveEditable { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("supplemental_language")]
        public object SupplementalLanguage { get; set; }

        [JsonProperty("self_thread", NullValueHandling = NullValueHandling.Ignore)]
        public SelfThread SelfThread { get; set; }
    }

    public partial class RetweetedStatusEntities
    {
        [JsonProperty("hashtags")]
        public object[] Hashtags { get; set; }

        [JsonProperty("symbols")]
        public object[] Symbols { get; set; }

        [JsonProperty("user_mentions")]
        public UserMention[] UserMentions { get; set; }

        [JsonProperty("urls")]
        public UrlUrl[] Urls { get; set; }

        [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
        public TentacledMedia[] Media { get; set; }
    }

    public partial class TentacledMedia
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("original_info")]
        public OriginalInfo OriginalInfo { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }
    }

    public partial class RetweetedStatusExtendedEntities
    {
        [JsonProperty("media")]
        public StickyMedia[] Media { get; set; }
    }

    public partial class StickyMedia
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("original_info")]
        public OriginalInfo OriginalInfo { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty("video_info", NullValueHandling = NullValueHandling.Ignore)]
        public VideoInfo VideoInfo { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }

        [JsonProperty("media_key")]
        public string MediaKey { get; set; }

        [JsonProperty("additional_media_info", NullValueHandling = NullValueHandling.Ignore)]
        public FluffyAdditionalMediaInfo AdditionalMediaInfo { get; set; }
    }

    public partial class FluffyAdditionalMediaInfo
    {
        [JsonProperty("monetizable")]
        public bool Monetizable { get; set; }
    }

    public partial class SelfThread
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }
    }

    public partial class RetweetedStatusUser
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("entities")]
        public PurpleEntities Entities { get; set; }

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
        public string[] AdvertiserAccountServiceLevels { get; set; }

        [JsonProperty("business_profile_state")]
        public string BusinessProfileState { get; set; }

        [JsonProperty("translator_type")]
        public string TranslatorType { get; set; }

        [JsonProperty("require_some_consent")]
        public bool RequireSomeConsent { get; set; }
    }

    public partial class PurpleEntities
    {
        [JsonProperty("url")]
        public FluffyUrl Url { get; set; }

        [JsonProperty("description")]
        public FluffyDescription Description { get; set; }
    }

    public partial class FluffyDescription
    {
        [JsonProperty("urls")]
        public DescriptionUrl[] Urls { get; set; }
    }

    public partial class DescriptionUrl
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class WelcomeUser
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

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("entities")]
        public SourceUserEntities Entities { get; set; }

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

        [JsonProperty("require_some_consent")]
        public bool RequireSomeConsent { get; set; }
    }*/
    public class TweetData
    {
        public string IdStr { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRetweet { get; set; }
        public User User { get; set; }
    }
    public class User
    {
        public string ScreenName { get; set; }
    }
    public partial class Coordinates
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] CoordinatesCoordinates { get; set; }
    }
    public partial class Place
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("place_type")]
        public string PlaceType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("contained_within")]
        public object[] ContainedWithin { get; set; }
    }
}
