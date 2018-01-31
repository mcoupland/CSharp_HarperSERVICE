using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MemberServices
{
    /// <summary>
    /// Summary description for MobileServices
    /// </summary>
    [WebService(Namespace = "http://andrewharper.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MobileServices : System.Web.Services.WebService
    {
        private string connectionstring = "SERVER=www.andrewharper.com;DATABASE=drupal;UID=mcoupland;PASSWORD=ecu18Fa`;";

        [WebMethod]
        public Hotel GetHotel(int HotelId)
        {
            string debugq = string.Empty;
            Hotel hotel = new Hotel();
            hotel.HotelId = HotelId;
            MySqlConnection connection = new MySqlConnection(connectionstring);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                command.CommandText = GetQueryGetHotel(HotelId);
                debugq = command.CommandText;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    hotel.Name = Reader.GetString("NAME");
                    hotel.MemberPrivileges = Reader.GetString("MEMBERPRIVILEGES");
                    hotel.Overview = Reader.GetString("OVERVIEW");
                    hotel.Rates = Reader.GetString("RATES");
                    hotel.Address1 = Reader.GetString("ADDRESS1");
                    hotel.Address2 = Reader.GetString("ADDRESS2");
                    hotel.City = Reader.GetString("CITY");
                    hotel.Province_State = Reader.GetString("PROVINCE_STATE");
                    hotel.PostalCode = Reader.GetString("POSTALCODE");
                    hotel.Country = Reader.GetString("COUNTRY");
                    hotel.IsAlliance = Reader.GetString("ISALLIANCE").ToLower() == "yes" ? true : false;
                    float.TryParse(Reader.GetString("RATING"), out hotel.Rating);
                    //hotel.Name = Reader.GetString("SPECIALOFFERID");
                    //hotel.Name = Reader.GetString("IMAGES");
                    //hotel.Name = Reader.GetString("THUMBNAILS");
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine(debugq);
            }
            finally
            {
                connection.Close();
            }
            return hotel;
        }

        [WebMethod]
        public List<Hotel> SearchHotels(string Name, string City, string Province_Region, string Country)
        {
            List<int> hotelids = new List<int>();
            List<Hotel> hotels = new List<Hotel>();
            MySqlConnection connection = new MySqlConnection(connectionstring);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                command.CommandText = GetQuerySearchHotel(Name);
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    hotels.Add(GetHotel(Reader.GetInt32(0)));
                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return hotels;

        }
        /*
        [WebMethod]
        public SpecialOffer GetSpecialOffer(int SpecialOfferId)
        {
            string debugq = string.Empty;
            SpecialOffer specialoffer = new SpecialOffer();
            specialoffer.OfferId = SpecialOfferId;
            MySqlConnection connection = new MySqlConnection(connectionstring);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                command.CommandText = GetQueryGetHotel(HotelId);
                debugq = command.CommandText;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    //specialoffer.Name = Reader.GetString("NAME");
                    //specialoffer.MemberPrivileges = Reader.GetString("MEMBERPRIVILEGES");
                    //specialoffer.Overview = Reader.GetString("OVERVIEW");
                    //specialoffer.Rates = Reader.GetString("RATES");
                    //specialoffer.Address1 = Reader.GetString("ADDRESS1");
                    //specialoffer.Address2 = Reader.GetString("ADDRESS2");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(debugq);
            }
            finally
            {
                connection.Close();
            }
            return specialoffer;
        }
        */
        [WebMethod]
        public List<SpecialOffer> SearchSpecialOffers(string Name, string City, string Province_Region, string Country)
        {
            List<SpecialOffer> specialoffers = new List<SpecialOffer>();
            specialoffers.Add(new SpecialOffer());
            specialoffers.Add(new SpecialOffer());
            specialoffers.Add(new SpecialOffer());
            return specialoffers;

        }

        [WebMethod]
        public MobileServicesResponse SubmitContactForm(string FirstName, string LastName, string HotelName, string HotelLocation, int RoomCount,
            DateTime CheckInDate, DateTime CheckOutDate, string Phone, string Email, bool IsMember, string Comments)
        {
            return new MobileServicesResponse();
        }

        private string GetQuerySearchHotel(string searchterm)
        {
            string query = @"
                select 
                 DISTINCT(hotel.nid)
                from node                                                          HOTEL 
                 left outer join content_field_member_privileges                    MEMBER_PRIVILEGES       on hotel.nid = member_privileges.nid                    and hotel.vid = member_privileges.vid
                 left outer join content_field_from_andrew_harper                   FROM_ANDREW             on hotel.nid = from_andrew.nid                          and hotel.vid = from_andrew.vid
                 left outer join content_field_rates                                RATES                   on hotel.nid = rates.nid                                and hotel.vid = rates.vid
                 left outer join content_field_location_geo                         ADDRESSLINK             on hotel.nid = addresslink.nid                          and hotel.vid = addresslink.vid
                 left outer join location                                           ADDRESS                 on addresslink.field_location_geo_lid = address.lid
                 left outer join content_type_hotels                                HOTELDATA               on hotel.nid = hoteldata.nid                            and hotel.vid = hoteldata.vid
                 left outer join content_field_ratings                              RATING                  on hotel.nid = rating.nid                               and hotel.vid = rating.vid
                 left outer join content_field_hotels                               OFFERSLINK              on hotel.nid = offerslink.field_hotels_nid
                 left outer join content_type_special_offers                        SPECIALOFFERS           on offerslink.nid = specialoffers.nid                   and offerslink.vid = specialoffers.vid
                 left outer join content_field_image                                IMAGELINK               on hotel.nid = imagelink.nid                            and hotel.vid = imagelink.vid 
                 left outer join files                                              IMAGES                  on imagelink.field_image_fid = images.fid
                where HOTEL.type = 'hotels'
                 and HOTEL.status = 1 
                 and 
                 (
                    /*exact hotel, others fuzzy*/
                    (
                        HOTEL.title = '{0}'    
                        or HOTEL.title like CONCAT('%', '{0}', '%') 
                        or ADDRESS.city like CONCAT('%', '{0}', '%') 
                        or ADDRESS.province like CONCAT('%', '{0}', '%') 
                        or ADDRESS.country like CONCAT('%', '{0}', '%')
                    )    
                    or
                    /*exact city, others fuzzy*/
                    (
                        ADDRESS.city = '{0}' 
                        or ADDRESS.city like CONCAT('%', '{0}', '%') 
                        or  HOTEL.title like CONCAT('%', '{0}', '%') 
                        or ADDRESS.province like CONCAT('%', '{0}', '%') 
                        or ADDRESS.country like CONCAT('%', '{0}', '%')  
                    )
                    or
                    /*#exact province, others fuzzy*/
                    (
                        ADDRESS.province = '{0}' 
                        or ADDRESS.province like CONCAT('%', '{0}', '%') 
                        or  HOTEL.title like CONCAT('%', '{0}', '%') 
                        or ADDRESS.city like CONCAT('%', '{0}', '%') 
                        or ADDRESS.country like CONCAT('%', '{0}', '%')  
                    )
                    or
                    /*exact country, others fuzzy*/
                    (
                        ADDRESS.country = '{0}' 
                        or ADDRESS.country like CONCAT('%', '{0}', '%') 
                        or  HOTEL.title like CONCAT('%', '{0}', '%') 
                        or ADDRESS.city like CONCAT('%', '{0}', '%') 
                        or ADDRESS.province like CONCAT('%', '{0}', '%')  
                    )
                 
                 )
                 order by HOTEL.nid;";
            return string.Format(query, searchterm).Replace("\r\n", "  ");
        }

        private string GetQueryGetHotel(int hotelid)
        {
            string query = @"
                            SELECT 
                            DISTINCT
                                 hotel.nid                                                          HOTELID
                                 , hotel.vid                                                        REVISION
                                 , hotel.title                                                      NAME
                                 , COALESCE(member_privileges.field_member_privileges_value, '')    MEMBERPRIVILEGES
                                 , COALESCE(from_andrew.field_from_andrew_harper_value, '')         OVERVIEW
                                 , COALESCE(rates.field_rates_value, '')                            RATES 
                                 , COALESCE(address.street, '')                                     ADDRESS1
                                 , COALESCE(address.additional, '')                                 ADDRESS2
                                 , COALESCE(address.city, '')                                       CITY
                                 , COALESCE(address.province, '')                                   PROVINCE_STATE
                                 , COALESCE(address.postal_code, '')                                POSTALCODE
                                 , COALESCE(address.country, '')                                    COUNTRY
                                 , COALESCE(hoteldata.field_alliance_hotel_value , 'No')            ISALLIANCE
                                 , COALESCE(rating.field_ratings_rating, -1)                        RATING
                             
                            FROM node                                                               HOTEL 
                                 left outer join content_field_member_privileges                    MEMBER_PRIVILEGES       on hotel.nid = member_privileges.nid                    and hotel.vid = member_privileges.vid
                                 left outer join content_field_from_andrew_harper                   FROM_ANDREW             on hotel.nid = from_andrew.nid                          and hotel.vid = from_andrew.vid
                                 left outer join content_field_rates                                RATES                   on hotel.nid = rates.nid                                and hotel.vid = rates.vid
                                 left outer join content_field_location_geo                         ADDRESSLINK             on hotel.nid = addresslink.nid                          and hotel.vid = addresslink.vid
                                 left outer join location                                           ADDRESS                 on addresslink.field_location_geo_lid = address.lid
                                 left outer join content_type_hotels                                HOTELDATA               on hotel.nid = hoteldata.nid                            and hotel.vid = hoteldata.vid
                                 left outer join content_field_ratings                              RATING                  on hotel.nid = rating.nid                               and hotel.vid = rating.vid
                             WHERE HOTEL.nid = {0};";
            return string.Format(query, hotelid).Replace("\r\n", "  ");
        }

        private string GetQueryGetImages(int hotelid)
        {
            string query = @"
                            SELECT 
                            DISTINCT
                                IMAGELINK.field_image_fid   IMAGEID
                            FROM 
                                node HOTEL
                                join content_field_image                                IMAGELINK               on hotel.nid = imagelink.nid                            and hotel.vid = imagelink.vid 
                            WHERE HOTEL.nid = {0};";
            return string.Format(query, hotelid).Replace("\r\n", "  ");
        }

        private string GetQueryGetSpecialOffers(int hotelid)
        {
            string query = @"
                            SELECT 
                            DISTINCT 
                                specialoffers.nid                                  SPECIALOFFERID
                            FROM 
                                node HOTEL
                                left outer join content_field_hotels                               OFFERSLINK              on hotel.nid = offerslink.field_hotels_nid
                                left outer join content_type_special_offers                        SPECIALOFFERS           on offerslink.nid = specialoffers.nid                   and offerslink.vid = specialoffers.vid
                            WHERE HOTEL.nid = {0};";
            return string.Format(query, hotelid).Replace("\r\n", "  ");
        }
    }

    public class Hotel
    {
        public int HotelId = 0;
        public string Name = string.Empty;
        public string MemberPrivileges = string.Empty;
        public string Overview = string.Empty;
        public string Rates = string.Empty;
        public string Address1 = string.Empty;
        public string Address2 = string.Empty;
        public string City = string.Empty;
        public string Province_State = string.Empty;
        public string PostalCode = string.Empty;
        public string Country = string.Empty;
        public List<string> Images = new List<string>();            
        public List<string> Thumbnails = new List<string>();            
        public bool IsAlliance = false;
        public float Rating = 0f;
        public List<int> SpecialOfferIds = new List<int>();

        public Hotel() { }       
    }

    public class SpecialOffer
    {
        public int OfferId = 0;
        public string Name = "Offer Name";
        public string Description = "Description";
        public int HotelId = 759;
        public List<string> Images = new List<string>();
        public List<string> Thumbnails = new List<string>();  

        public SpecialOffer()
        {             
            Images.Add("http://www.andrewharper.com/sites/default/files/imagecache/hotel_rotator/SiteImages/HOTELS/1485/sliders/1485_Les_Mars_Hotel_bth.jpg");
            Images.Add("http://www.andrewharper.com/sites/default/files/imagecache/hotel_rotator/SiteImages/HOTELS/1485/sliders/1485_Les_Mars_Hotel_bed.jpg");
            Thumbnails.Add("http://www.andrewharper.com/sites/default/files/imagecache/hotel_rotator/SiteImages/HOTELS/1485/sliders/1485_Les_Mars_Hotel_bth.jpg");
            Thumbnails.Add("http://www.andrewharper.com/sites/default/files/imagecache/hotel_rotator/SiteImages/HOTELS/1485/sliders/1485_Les_Mars_Hotel_bed.jpg");            
        }
    }

    public class ContactForm
    {
        public string FirstName;
        public string LastName;
        public string HotelName;
        public string HotelLocation;
        public int RoomCount;
        public DateTime CheckInDate;
        public DateTime CheckOutDate;
        public string Phone;
        public string Email;
        public bool IsMember;
        public string Comments;

        public ContactForm()
        {
        }
    }

    public class MobileServicesResponse
    {
        public bool IsSuccess = true;
        public string Message = "Contact Form Submitted.";

    }

}
