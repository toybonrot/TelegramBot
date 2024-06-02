using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TelegramBot.Models.SearchAttraction;

namespace TelegramBot.Models
{
    public class AttractionDetails
    {
        public bool status { get; set; }
        public string message { get; set; }
        public long timestamp { get; set; }
        public DataAtt2 data { get; set; }
    }
    public class DataAtt2
    {
        public string __typename { get; set; }
        public object[] accessibility { get; set; }
        public object additionalBookingInfo { get; set; }
        public string additionalInfo { get; set; }
        public Addresses addresses { get; set; }
        public Applicableterm[] applicableTerms { get; set; }
        public object audioSupportedLanguages { get; set; }
        public CancellationpolicyAtt cancellationPolicy { get; set; }
        public object[] covid { get; set; }
        public string description { get; set; }
        public object[] dietOptions { get; set; }
        public FlagAtt[] flags { get; set; }
        public string[] guideSupportedLanguages { get; set; }
        public string[] healthSafety { get; set; }
        public string id { get; set; }
        public bool isBookable { get; set; }
        public Label[] labels { get; set; }
        public string name { get; set; }
        public string[] notIncluded { get; set; }
        public OfferAtt[] offers { get; set; }
        public Onsiterequirements onSiteRequirements { get; set; }
        public string operatedBy { get; set; }
        public PhotoAtt[] photos { get; set; }
        public PrimaryphotoAtt primaryPhoto { get; set; }
        public RepresentativepriceAtt representativePrice { get; set; }
        public object[] restrictions { get; set; }
        public Reviews reviews { get; set; }
        public ReviewsstatsAtt reviewsStats { get; set; }
        public string shortDescription { get; set; }
        public string slug { get; set; }
        public SupportedfeaturesAtt supportedFeatures { get; set; }
        public object uniqueSellingPoints { get; set; }
        public UfidetailsAtt ufiDetails { get; set; }
        public string[] whatsIncluded { get; set; }
    }

    public class Addresses
    {
        public string __typename { get; set; }
        public Arrival[] arrival { get; set; }
        public object attraction { get; set; }
        public Departure[] departure { get; set; }
        public object entrance { get; set; }
        public object guestPickup { get; set; }
        public object meeting { get; set; }
        public object pickup { get; set; }
    }

    public class Arrival
    {
        public string __typename { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public object googlePlaceId { get; set; }
        public string id { get; set; }
        public object instructions { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string locationType { get; set; }
        public object publicTransport { get; set; }
        public object zip { get; set; }
    }

    public class Departure
    {
        public string __typename { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public object googlePlaceId { get; set; }
        public string id { get; set; }
        public string instructions { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string locationType { get; set; }
        public object publicTransport { get; set; }
        public object zip { get; set; }
    }

    public class CancellationpolicyAtt
    {
        public string __typename { get; set; }
        public bool hasFreeCancellation { get; set; }
    }

    public class Onsiterequirements
    {
        public string __typename { get; set; }
        public object voucherPrintingRequired { get; set; }
    }

    public class PrimaryphotoAtt
    {
        public string __typename { get; set; }
        public string small { get; set; }
    }

    public class RepresentativepriceAtt
    {
        public string __typename { get; set; }
        public double chargeAmount { get; set; }
        public string currency { get; set; }
        public double publicAmount { get; set; }
    }

    public class Reviews
    {
        public string __typename { get; set; }
        public int total { get; set; }
        public Review[] reviews { get; set; }
    }

    public class Review
    {
        public string __typename { get; set; }
        public string id { get; set; }
        public bool? rating { get; set; }
        public string content { get; set; }
        public long epochMs { get; set; }
        public string language { get; set; }
        public int numericRating { get; set; }
        public object providerName { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        public string __typename { get; set; }
        public string name { get; set; }
        public string cc1 { get; set; }
        public string avatar { get; set; }
    }

    public class ReviewsstatsAtt
    {
        public string __typename { get; set; }
        public int allReviewsCount { get; set; }
        public string percentage { get; set; }
        public CombinednumericstatsAtt combinedNumericStats { get; set; }
    }

    public class CombinednumericstatsAtt
    {
        public string __typename { get; set; }
        public float average { get; set; }
        public int total { get; set; }
    }

    public class SupportedfeaturesAtt
    {
        public string __typename { get; set; }
        public bool nativeApp { get; set; }
        public bool nativeAppBookProcess { get; set; }
        public bool liveAvailabilityCheckSupported { get; set; }
    }

    public class UfidetailsAtt
    {
        public string __typename { get; set; }
        public int ufi { get; set; }
        public string bCityName { get; set; }
        public UrlAtt url { get; set; }
    }

    public class UrlAtt
    {
        public string __typename { get; set; }
        public string country { get; set; }
    }

    public class Applicableterm
    {
        public string __typename { get; set; }
        public string policyProvider { get; set; }
        public string privacyPolicyUrl { get; set; }
        public string termsUrl { get; set; }
    }

    public class FlagAtt
    {
        public string __typename { get; set; }
        public string flag { get; set; }
        public bool value { get; set; }
        public int rank { get; set; }
    }

    public class Label
    {
        public string __typename { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }

    public class OfferAtt
    {
        public string __typename { get; set; }
        public string availabilityType { get; set; }
        public string id { get; set; }
    }

    public class PhotoAtt
    {
        public string __typename { get; set; }
        public string small { get; set; }
        public string medium { get; set; }
        public bool isPrimary { get; set; }
    }
}
