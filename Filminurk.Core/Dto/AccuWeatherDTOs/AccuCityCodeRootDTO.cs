using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto.AccuWeatherDTOs
{
    public class AccuCityCodeRootDTO
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public int Version { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Rank { get; set; }
        public string LocalizedName {  get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
        public string PrimaryPostalCode {  get; set; } = string.Empty;
        public Region? Region { get; set; }
        public Country? Country { get; set; }
        public AdministrativeArea? AdministrativeArea { get; set; }
        public TimeZone? TimeZone { get; set; }
        public GeoPosition? GeoPosition { get; set; }
        public bool IsAlias { get; set; }
        public SupplementalAdminArea[]? SupplementalAdminAreas { get; set; }
        public string[]? DataSets { get; set; }

       


    }

 
    

    public class AdministrativeArea



    {
        public int Level { get; set; }
        public LocalizedType LocalizedType { get; set; }
        public EnglishType EnglishType { get; set; }
        public string CountryID { get; set; }
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class Country
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class Details
    {
        public string Key { get; set; }
        public string StationCode { get; set; }
        public int StationGmtOffset { get; set; }
        public string BandMap { get; set; }
        public string Climo { get; set; }
        public string LocalRadar { get; set; }
        public string MediaRegion { get; set; }
        public string Metar { get; set; }
        public string NXMetro { get; set; }
        public string NXState { get; set; }
        public int Population { get; set; }
        public string PrimaryWarningCountyCode { get; set; }
        public string PrimaryWarningZoneCode { get; set; }
        public string Satellite { get; set; }
        public string Synoptic { get; set; }
        public string MarineStation { get; set; }
        public int MarineStationGMTOffset { get; set; }
        public string VideoCode { get; set; }
        public string LocationStem { get; set; }
        public DMA DMA { get; set; }
        public PartnerID PartnerID { get; set; }
        public List<Source> Sources { get; set; }
        public string CanonicalPostalCode { get; set; }
        public string CanonicalLocationKey { get; set; }
    }

    public class DMA
    {
        public string ID { get; set; }
        public string EnglishName { get; set; }
    }

    public class Elevation
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class EnglishType
    {
    }

    public class GeoPosition
    {
        public Elevation Elevation { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }

    public class Imperial
    {
        public int UnitType { get; set; }
        public int Value { get; set; }
        public string Unit { get; set; }
    }

    public class LocalizedType
    {
    }

    public class Metric
    {
        public int UnitType { get; set; }
        public int Value { get; set; }
        public string Unit { get; set; }
    }

    public class ParentCity
    {
        public string Key { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class PartnerID
    {
    }

    public class PartnerSourceUrl
    {
    }

    public class Region
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class Root
    {
        public string PrimaryPostalCode { get; set; }
        public Region Region { get; set; }
        public TimeZone TimeZone { get; set; }
        public GeoPosition GeoPosition { get; set; }
        public bool IsAlias { get; set; }
        public ParentCity ParentCity { get; set; }
        public List<SupplementalAdminArea> SupplementalAdminAreas { get; set; }
        public List<string> DataSets { get; set; }
        public Details Details { get; set; }
        public string EnglishName { get; set; }
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public Country Country { get; set; }
        public AdministrativeArea AdministrativeArea { get; set; }
    }

    public class Source
    {
        public string DataType { get; set; }
        public string Source { get; set; }
        public int SourceId { get; set; }
        public PartnerSourceUrl PartnerSourceUrl { get; set; }
    }

    public class SupplementalAdminArea
    {
        public int Level { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class TimeZone
    {
        public string Code { get; set; }
        public DateTime NextOffsetChange { get; set; }
        public string Name { get; set; }
        public int GmtOffset { get; set; }
        public bool IsDaylightSaving { get; set; }
    }
}
