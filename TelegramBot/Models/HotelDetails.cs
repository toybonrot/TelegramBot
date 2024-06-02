using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class HotelDetails
    {
        public DataInfo data { get; set; }
        public class DataInfo
        {
            public int ufi { get; set; }
            public int hotel_id { get; set; }
            public string hotel_name { get; set; }
            public string url { get; set; }
            public string accommodation_type_name { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string district { get; set; }
            public float distance_to_cc { get; set; }
            public string currency_code { get; set; }
            public int available_rooms { get; set; }
            public string average_room_size_for_ufi_m2 { get; set; }
            public int hotel_include_breakfast { get; set; }
            public string[] family_facilities { get; set; }
            public Product_Price_Breakdown product_price_breakdown { get; set; }
            public Composite_Price_Breakdown composite_price_breakdown { get; set; }
            public Property_Highlight_Strip[] property_highlight_strip { get; set; }
            public Top_Ufi_Benefits[] top_ufi_benefits { get; set; }
            public Breakfast_Review_Score breakfast_review_score { get; set; }
            public Wifi_Review_Score wifi_review_score { get; set; }
            public Hotel_Important_Information_With_Codes[] hotel_important_information_with_codes { get; set; }
            public Rooms rooms { get; set; }
        }

        public class Product_Price_Breakdown
        {
            public Charges_Details charges_details { get; set; }
            public int nr_stays { get; set; }
            public int has_long_stays_weekly_rate_price { get; set; }
            public int has_long_stays_monthly_rate_price { get; set; }
            public object[] benefits { get; set; }
            public Excluded_Amount excluded_amount { get; set; }
            public Gross_Amount_Hotel_Currency gross_amount_hotel_currency { get; set; }
            public Included_Taxes_And_Charges_Amount included_taxes_and_charges_amount { get; set; }
            public Item[] items { get; set; }
            public Gross_Amount_Per_Night gross_amount_per_night { get; set; }
            public All_Inclusive_Amount all_inclusive_amount { get; set; }
            public Net_Amount net_amount { get; set; }
            public Gross_Amount gross_amount { get; set; }
        }

        public class Charges_Details
        {
            public Amount amount { get; set; }
            public string mode { get; set; }
            public string translated_copy { get; set; }
        }

        public class Amount
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class Excluded_Amount
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Gross_Amount_Hotel_Currency
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class Included_Taxes_And_Charges_Amount
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class Gross_Amount_Per_Night
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class All_Inclusive_Amount
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Net_Amount
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Gross_Amount
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Item
        {
            public string name { get; set; }
            public Item_Amount item_amount { get; set; }
            public string kind { get; set; }
            public Base _base { get; set; }
            public string inclusion_type { get; set; }
            public object details { get; set; }
        }

        public class Item_Amount
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Base
        {
            public int base_amount { get; set; }
            public string kind { get; set; }
        }

        public class Composite_Price_Breakdown
        {
            public Included_Taxes_And_Charges_Amount1 included_taxes_and_charges_amount { get; set; }
            public Gross_Amount_Hotel_Currency1 gross_amount_hotel_currency { get; set; }
            public int has_long_stays_weekly_rate_price { get; set; }
            public int has_long_stays_monthly_rate_price { get; set; }
            public object[] benefits { get; set; }
            public Excluded_Amount1 excluded_amount { get; set; }
            public Charges_Details1 charges_details { get; set; }
            public All_Inclusive_Amount1 all_inclusive_amount { get; set; }
            public Net_Amount1 net_amount { get; set; }
            public Gross_Amount1 gross_amount { get; set; }
            public Item1[] items { get; set; }
            public Gross_Amount_Per_Night1 gross_amount_per_night { get; set; }
        }

        public class Included_Taxes_And_Charges_Amount1
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Gross_Amount_Hotel_Currency1
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Excluded_Amount1
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Charges_Details1
        {
            public string mode { get; set; }
            public Amount1 amount { get; set; }
            public string translated_copy { get; set; }
        }

        public class Amount1
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class All_Inclusive_Amount1
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Net_Amount1
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Gross_Amount1
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Gross_Amount_Per_Night1
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Item1
        {
            public string name { get; set; }
            public Item_Amount1 item_amount { get; set; }
            public string kind { get; set; }
            public Base1 _base { get; set; }
            public string inclusion_type { get; set; }
            public object details { get; set; }
        }

        public class Item_Amount1
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Base1
        {
            public string kind { get; set; }
            public int base_amount { get; set; }
        }

        public class Sustainability
        {
            public Sustainability_Page sustainability_page { get; set; }
            public Hotel_Page hotel_page { get; set; }
            public Sustainability_Level sustainability_level { get; set; }
        }

        public class Sustainability_Page
        {
            public string tier { get; set; }
            public string description { get; set; }
            public int sustainable_facility_count { get; set; }
            public string title { get; set; }
            public string level_details { get; set; }
            public string level { get; set; }
            public string cta { get; set; }
            public Effort[] efforts { get; set; }
            public int has_programmes { get; set; }
        }

        public class Effort
        {
            public string icon { get; set; }
            public string title { get; set; }
            public string[] steps { get; set; }
        }

        public class Hotel_Page
        {
            public int has_badge { get; set; }
            public string title { get; set; }
            public string cta { get; set; }
            public string description { get; set; }
        }

        public class Sustainability_Level
        {
            public string name { get; set; }
            public int id { get; set; }
            public string title { get; set; }
        }

        public class Facilities_Block
        {
            public Facility[] facilities { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }

        public class Facility
        {
            public string name { get; set; }
            public string icon { get; set; }
        }

        public class Languages_Spoken
        {
            public string[] languagecode { get; set; }
        }

        public class Breakfast_Review_Score
        {
            public float rating { get; set; }
            public float review_score { get; set; }
            public string review_snippet { get; set; }
            public string review_score_word { get; set; }
            public int review_number { get; set; }
            public int review_count { get; set; }
        }

        public class Wifi_Review_Score
        {
            public float rating { get; set; }
        }

        public class Min_Room_Distribution
        {
            public object[] children { get; set; }
            public int adults { get; set; }
        }

        public class Booking_Home
        {
        }

        public class Aggregated_Data
        {
            public int has_refundable { get; set; }
            public int has_nonrefundable { get; set; }
            public int has_seating { get; set; }
            public int has_kitchen { get; set; }
            public Common_Kitchen_Fac[] common_kitchen_fac { get; set; }
        }

        public class Common_Kitchen_Fac
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        public class Last_Reservation
        {
            public object country { get; set; }
            public object countrycode { get; set; }
            public string time { get; set; }
        }

        public class Hotel_Text
        {
        }

        public class Rooms
        {
            public _19160501 _19160501 { get; set; }
        }

        public class _19160501
        {
            public Private_Bathroom_Highlight private_bathroom_highlight { get; set; }
            public Facility1[] facilities { get; set; }
            public Photo[] photos { get; set; }
            public int is_high_floor_guaranteed { get; set; }
            public Bed_Configurations[] bed_configurations { get; set; }
            public Cribs_Extra_Beds cribs_extra_beds { get; set; }
            public Highlight[] highlights { get; set; }
            public Children_And_Beds_Text children_and_beds_text { get; set; }
            public int private_bathroom_count { get; set; }
            public string description { get; set; }
        }

        public class Private_Bathroom_Highlight
        {
            public int has_highlight { get; set; }
        }

        public class Cribs_Extra_Beds
        {
            public Extra_Beds extra_beds { get; set; }
        }

        public class Extra_Beds
        {
            public int all_free { get; set; }
            public int max_count { get; set; }
            public int[] ages { get; set; }
        }

        public class Children_And_Beds_Text
        {
            public Age_Intervals[] age_intervals { get; set; }
            public int allow_children { get; set; }
            public Children_At_The_Property[] children_at_the_property { get; set; }
            public Cribs_And_Extra_Beds[] cribs_and_extra_beds { get; set; }
        }

        public class Age_Intervals
        {
            public string[][] types_by_price { get; set; }
            public Group_By_Price group_by_price { get; set; }
            public int max_age { get; set; }
            public int min_age { get; set; }
            public Crib crib { get; set; }
            public Extra_Bed extra_bed { get; set; }
        }

        public class Group_By_Price
        {
            public string[] freeper_night0 { get; set; }
            public string[] fixedper_night2180 { get; set; }
        }

        public class Crib
        {
            public int id { get; set; }
            public int price_type_n { get; set; }
            public string price_mode { get; set; }
            public string price_type { get; set; }
            public int price { get; set; }
            public int price_mode_n { get; set; }
        }

        public class Extra_Bed
        {
            public int id { get; set; }
            public string price_mode { get; set; }
            public int price_type_n { get; set; }
            public string price { get; set; }
            public int price_mode_n { get; set; }
            public string price_type { get; set; }
        }

        public class Children_At_The_Property
        {
            public string text { get; set; }
            public int highlight { get; set; }
        }

        public class Cribs_And_Extra_Beds
        {
            public int highlight { get; set; }
            public string text { get; set; }
        }

        public class Facility1
        {
            public string name { get; set; }
            public int alt_facilitytype_id { get; set; }
            public int id { get; set; }
            public string facilitytype_name { get; set; }
            public int facilitytype_id { get; set; }
            public string alt_facilitytype_name { get; set; }
        }

        public class Photo
        {
            public string url_max750 { get; set; }
            public string last_update_date { get; set; }
            public string url_square180 { get; set; }
            public string url_max1280 { get; set; }
            public float ratio { get; set; }
            public string url_original { get; set; }
            public string url_square60 { get; set; }
            public string url_640x200 { get; set; }
            public int photo_id { get; set; }
            public string url_max300 { get; set; }
            public int new_order { get; set; }
        }

        public class Bed_Configurations
        {
            public Bed_Types[] bed_types { get; set; }
        }

        public class Bed_Types
        {
            public int bed_type { get; set; }
            public string name_with_count { get; set; }
            public int count { get; set; }
            public string description { get; set; }
            public string name { get; set; }
            public string description_imperial { get; set; }
            public object description_localized { get; set; }
        }

        public class Highlight
        {
            public string icon { get; set; }
            public string translated_name { get; set; }
            public int id { get; set; }
        }

        public class Property_Highlight_Strip
        {
            public string name { get; set; }
            public Icon_List[] icon_list { get; set; }
        }

        public class Icon_List
        {
            public int size { get; set; }
            public string icon { get; set; }
        }

        public class Top_Ufi_Benefits
        {
            public string translated_name { get; set; }
            public string icon { get; set; }
        }

        public class Info_Banners
        {
            public Primary_Action primary_action { get; set; }
            public string type { get; set; }
            public int is_default_collapsed { get; set; }
            public int priority { get; set; }
            public Secondary_Action secondary_action { get; set; }
            public string title { get; set; }
            public int position_to_inject { get; set; }
            public int collapsible { get; set; }
            public string id { get; set; }
            public int show_after_dismissed_time { get; set; }
            public string[] messages { get; set; }
            public int show_after_dismissed { get; set; }
            public int dismissable { get; set; }
        }

        public class Primary_Action
        {
            public string action_context { get; set; }
            public string action { get; set; }
            public string text { get; set; }
        }

        public class Secondary_Action
        {
        }

        public class Free_Facilities_Cancel_Breakfast
        {
            public int facility_id { get; set; }
        }

        public class Room_Recommendation
        {
            public int children { get; set; }
            public int babies { get; set; }
            public string block_id { get; set; }
            public int extra_babycots_price { get; set; }
            public int extra_beds_for_children_price { get; set; }
            public int number_of_extra_babycots { get; set; }
            public int adults { get; set; }
            public int total_extra_bed_price { get; set; }
            public int extra_beds_for_children_price_in_hotel_currency { get; set; }
            public int number_of_extra_beds_for_children { get; set; }
            public int extra_babycots_price_in_hotel_currency { get; set; }
            public int number_of_extra_beds_for_adults { get; set; }
            public int total_extra_bed_price_in_hotel_currency { get; set; }
            public int extra_beds_for_adults_price { get; set; }
            public int number_of_extra_beds_and_babycots_total { get; set; }
            public int extra_beds_for_adults_price_in_hotel_currency { get; set; }
        }

        public class Hotel_Important_Information_With_Codes
        {
            public string phrase { get; set; }
            public int executing_phase { get; set; }
            public int sentence_id { get; set; }
        }

        public class Block
        {
            public int nr_children { get; set; }
            public int refundable { get; set; }
            public string room_name { get; set; }
            public int room_id { get; set; }
            public string name_without_policy { get; set; }
            public int number_of_bedrooms { get; set; }
            public int refundable_until_epoch { get; set; }
            public Paymentterms paymentterms { get; set; }
            public int room_count { get; set; }
            public int room_surface_in_m2 { get; set; }
            public int smoking { get; set; }
            public int max_children_free_age { get; set; }
            public string mealplan { get; set; }
            public int is_domestic_rate { get; set; }
            public int must_reserve_free_parking { get; set; }
            public object[] children_ages { get; set; }
            public int is_block_fit { get; set; }
            public string max_occupancy { get; set; }
            public int genius_discount_percentage { get; set; }
            public object[] bh_room_highlights { get; set; }
            public int extrabed_available { get; set; }
            public int deposit_required { get; set; }
            public int breakfast_included { get; set; }
            public string block_id { get; set; }
            public int roomtype_id { get; set; }
            public int all_inclusive { get; set; }
            public string refundable_until { get; set; }
            public string name { get; set; }
            public int is_last_minute_deal { get; set; }
            public int can_reserve_free_parking { get; set; }
            public int babycots_available { get; set; }
            public int is_flash_deal { get; set; }
            public float room_surface_in_feet2 { get; set; }
            public object babycots_available_amount { get; set; }
            public int number_of_bathrooms { get; set; }
            public int nr_adults { get; set; }
            public int half_board { get; set; }
            public object extrabed_available_amount { get; set; }
            public int is_secret_deal { get; set; }
            public int pod_ios_migrate_policies_to_smp_fullon { get; set; }
            public int is_smart_deal { get; set; }
            public Block_Text block_text { get; set; }
            public Deals deals { get; set; }
            public object is_genius_deal { get; set; }
            public int full_board { get; set; }
            public int max_children_free { get; set; }
        }

        public class Paymentterms
        {
            public Cancellation cancellation { get; set; }
            public Prepayment prepayment { get; set; }
        }

        public class Cancellation
        {
            public string description { get; set; }
            public Info info { get; set; }
            public int guaranteed_non_refundable { get; set; }
            public string type_translation { get; set; }
            public string bucket { get; set; }
            public string type { get; set; }
            public int non_refundable_anymore { get; set; }
            public Timeline timeline { get; set; }
        }

        public class Info
        {
            public object time_before_midnight { get; set; }
            public string timezone { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public string date_before { get; set; }
            public string is_midnight { get; set; }
            public int refundable { get; set; }
            public string date_raw { get; set; }
            public string timezone_offset { get; set; }
            public string date_before_raw { get; set; }
        }

        public class Timeline
        {
            public string u_currency_code { get; set; }
            public string currency_code { get; set; }
            public Stage[] stages { get; set; }
            public string policygroup_instance_id { get; set; }
            public int nr_stages { get; set; }
        }

        public class Stage
        {
            public string limit_from { get; set; }
            public string text { get; set; }
            public string limit_from_date { get; set; }
            public int fee_remaining { get; set; }
            public string fee_pretty { get; set; }
            public string stage_translation { get; set; }
            public string text_refundable { get; set; }
            public int b_number { get; set; }
            public string u_stage_fee_pretty { get; set; }
            public string limit_from_raw { get; set; }
            public int fee { get; set; }
            public int is_effective { get; set; }
            public string limit_until_time { get; set; }
            public string b_state { get; set; }
            public int effective_number { get; set; }
            public int fee_rounded { get; set; }
            public int current_stage { get; set; }
            public string u_fee_remaining_pretty { get; set; }
            public string u_fee_remaining { get; set; }
            public string date_until { get; set; }
            public int is_free { get; set; }
            public string limit_until { get; set; }
            public string u_stage_fee { get; set; }
            public string limit_until_date { get; set; }
            public string u_fee { get; set; }
            public string limit_until_raw { get; set; }
            public string u_fee_pretty { get; set; }
            public string limit_from_time { get; set; }
            public string stage_fee_pretty { get; set; }
            public string fee_remaining_pretty { get; set; }
            public string limit_timezone { get; set; }
            public int stage_fee { get; set; }
            public string date_from { get; set; }
            public float amount { get; set; }
            public string amount_pretty { get; set; }
        }

        public class Prepayment
        {
            public string description { get; set; }
            public string simple_translation { get; set; }
            public string type_extended { get; set; }
            public Info1 info { get; set; }
            public string extended_type_translation { get; set; }
            public string type { get; set; }
            public string type_translation { get; set; }
            public Timeline1 timeline { get; set; }
        }

        public class Info1
        {
            public object timezone_offset { get; set; }
            public string refundable { get; set; }
            public object is_midnight { get; set; }
            public object time { get; set; }
            public object date { get; set; }
            public object date_before { get; set; }
            public int prepayment_at_booktime { get; set; }
            public object timezone { get; set; }
            public object time_before_midnight { get; set; }
        }

        public class Timeline1
        {
            public string u_currency_code { get; set; }
            public string currency_code { get; set; }
            public string policygroup_instance_id { get; set; }
            public Stage1[] stages { get; set; }
            public int nr_stages { get; set; }
        }

        public class Stage1
        {
            public string amount_pretty { get; set; }
            public int fee_remaining { get; set; }
            public string limit_from_date { get; set; }
            public string fee_pretty { get; set; }
            public string text { get; set; }
            public string limit_from { get; set; }
            public int current_stage { get; set; }
            public int effective_number { get; set; }
            public string b_state { get; set; }
            public string limit_until_time { get; set; }
            public int fee_rounded { get; set; }
            public string limit_from_raw { get; set; }
            public int fee { get; set; }
            public string u_stage_fee_pretty { get; set; }
            public string amount { get; set; }
            public int is_effective { get; set; }
            public int b_number { get; set; }
            public string u_stage_fee { get; set; }
            public int is_free { get; set; }
            public string limit_until { get; set; }
            public string u_fee_remaining { get; set; }
            public string u_fee_remaining_pretty { get; set; }
            public int stage_fee { get; set; }
            public string limit_timezone { get; set; }
            public string stage_fee_pretty { get; set; }
            public string fee_remaining_pretty { get; set; }
            public string limit_from_time { get; set; }
            public string u_fee_pretty { get; set; }
            public string u_fee { get; set; }
            public string limit_until_date { get; set; }
            public string limit_until_raw { get; set; }
            public int after_checkin { get; set; }
        }

        public class Block_Text
        {
            public Policy[] policies { get; set; }
        }

        public class Policy
        {
            public string _class { get; set; }
            public string content { get; set; }
            public string mealplan_vector { get; set; }
        }

        public class Deals
        {
            public Deal_Attributes deal_attributes { get; set; }
        }

        public class Deal_Attributes
        {
            public int has_secret_channel_option { get; set; }
        }

    }
}
