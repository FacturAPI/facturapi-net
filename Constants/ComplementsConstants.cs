using System.Collections.Generic;

namespace Facturapi
{
    public static class CustomsRegimes
    {
        public const string DEFINITIVE_IMPORT = "IMD";
        public const string DEFINITIVE_EXPORT = "EXD";
        public const string INTERNAL_MERCHANDISE_TRANSIT = "ITR";
        public const string INTERNAL_MERCHANDISE_TRANSIT_FOR_EXPORT = "ITE";
        public const string EXTERNAL_MERCHANDISE_TRANSIT = "ETR";
        public const string EXTERNAL_MERCHANDISE_TRANSIT_FOR_EXPORT = "ETE";
        public const string FISCAL_WAREHOUSE = "DFI";
        public const string STRATEGIC_FISCAL_ENCLOSURE = "RFE";
        public const string FISCAL_ENCLOSURE = "RFS";
        public const string CUSTOMS_TRANSIT = "TRA";
    }

    public static class CustomsRegimesDescription
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [CustomsRegimes.DEFINITIVE_IMPORT] = "Importación definitiva",
            [CustomsRegimes.DEFINITIVE_EXPORT] = "Exportación definitiva",
            [CustomsRegimes.INTERNAL_MERCHANDISE_TRANSIT] = "Tránsito interno de mercancías",
            [CustomsRegimes.INTERNAL_MERCHANDISE_TRANSIT_FOR_EXPORT] = "Tránsito interno de mercancías para exportación",
            [CustomsRegimes.EXTERNAL_MERCHANDISE_TRANSIT] = "Tránsito externo de mercancías",
            [CustomsRegimes.EXTERNAL_MERCHANDISE_TRANSIT_FOR_EXPORT] = "Tránsito externo de mercancías para exportación",
            [CustomsRegimes.FISCAL_WAREHOUSE] = "Depósito fiscal",
            [CustomsRegimes.STRATEGIC_FISCAL_ENCLOSURE] = "Recinto fiscalizado estratégico",
            [CustomsRegimes.FISCAL_ENCLOSURE] = "Recinto fiscalizado",
            [CustomsRegimes.CUSTOMS_TRANSIT] = "Tránsito aduanero"
        };
    }

    public static class CveTransporte
    {
        public const string AUTOTRANSPORT = "01";
        public const string NAVY_TRANSPORT = "02";
        public const string AIRLINE_TRANSPORT = "03";
        public const string RAIL_TRANSPORT = "04";
        public const string OTHER = "05";
    }

    public static class CveTransportDescription
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [CveTransporte.AUTOTRANSPORT] = "Autotransporte",
            [CveTransporte.NAVY_TRANSPORT] = "Transporte Marítimo",
            [CveTransporte.AIRLINE_TRANSPORT] = "Transporte Aéreo",
            [CveTransporte.RAIL_TRANSPORT] = "Transporte Ferroviario",
            [CveTransporte.OTHER] = "Otro"
        };
    }

    public static class TipoEstacion
    {
        public const string NATIONAL_ORIGIN = "01";
        public const string INTERMEDIATE = "02";
        public const string FINAL_DESTINATION = "03";
    }

    public static class TipoEstacionDescription
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [TipoEstacion.NATIONAL_ORIGIN] = "Origen Nacional",
            [TipoEstacion.INTERMEDIATE] = "Intermedia",
            [TipoEstacion.FINAL_DESTINATION] = "Destino Final Nacional"
        };
    }

    public static class PermisoSct
    {
        public const string FEDERAL_TRANSPORT_OF_LOAD = "TPAF01";
        public const string PRIVATE_TRANSPORT_OF_LOAD = "TPAF02";
        public const string FEDERAL_SPECIALIZED_HAZARDOUS_MATERIALS = "TPAF03";
        public const string TRANSPORT_OF_AUTOMOBILES = "TPAF04";
        public const string TRANSPORT_OF_HEAVY_LOAD_UP_TO_90_TONS = "TPAF05";
        public const string TRANSPORT_OF_SPECIALIZED_HEAVY_LOAD_OVER_90_TONS = "TPAF06";
        public const string PRIVATE_HAZARDOUS_MATERIALS_TRANSPORT = "TPAF07";
        public const string INTERNATIONAL_LONG_HAUL_TRANSPORT = "TPAF08";
        public const string INTERNATIONAL_SPECIALIZED_HAZARDOUS_LONG_HAUL = "TPAF09";
        public const string FEDERAL_TRANSPORT_US_BORDER_ZONE = "TPAF10";
        public const string FEDERAL_SPECIALIZED_US_BORDER_ZONE = "TPAF11";
        public const string AUXILIARY_TOWING_SERVICE = "TPAF12";
        public const string AUXILIARY_TOWING_AND_STORAGE_SERVICE = "TPAF13";
        public const string PACKAGING_AND_COURIER_SERVICE = "TPAF14";
        public const string SPECIAL_TRANSPORT_INDUSTRIAL_CRANES_UP_TO_90_TONS = "TPAF15";
        public const string FEDERAL_RENTAL_COMPANIES_SERVICE = "TPAF16";
        public const string VEHICLE_MOVERS_NEW_VEHICLES = "TPAF17";
        public const string MANUFACTURERS_DISTRIBUTORS_NEW_VEHICLES = "TPAF18";
        public const string AUTHORIZATION_DOUBLE_ARTICULATED_TRUCK = "TPAF19";
        public const string FEDERAL_SPECIALIZED_FUNDS_AND_VALUES = "TPAF20";
        public const string TEMPORARY_CABOTAGE_NAVIGATION = "TPTM01";
        public const string NATIONAL_INTERNATIONAL_REGULAR_SERVICE_MEXICAN = "TPTA01";
        public const string FOREIGN_COMPANIES_REGULAR_AIR_SERVICE = "TPTA02";
        public const string NATIONAL_INTERNATIONAL_CHARTER_SERVICE = "TPTA03";
        public const string NATIONAL_INTERNATIONAL_AIR_TAXI_SERVICE = "TPTA04";
        public const string NOT_IN_CATALOG = "TPXX00";
    }

    public static class PermisoSctDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [PermisoSct.FEDERAL_TRANSPORT_OF_LOAD] = "Autotransporte Federal de carga general.",
            [PermisoSct.PRIVATE_TRANSPORT_OF_LOAD] = "Transporte privado de carga.",
            [PermisoSct.FEDERAL_SPECIALIZED_HAZARDOUS_MATERIALS] = "Autotransporte Federal de Carga Especializada de materiales y residuos peligrosos.",
            [PermisoSct.TRANSPORT_OF_AUTOMOBILES] = "Transporte de automóviles sin rodar en vehículo tipo góndola.",
            [PermisoSct.TRANSPORT_OF_HEAVY_LOAD_UP_TO_90_TONS] = "Transporte de carga de gran peso y/o volumen de hasta 90 toneladas.",
            [PermisoSct.TRANSPORT_OF_SPECIALIZED_HEAVY_LOAD_OVER_90_TONS] = "Transporte de carga especializada de gran peso y/o volumen de más 90 toneladas.",
            [PermisoSct.PRIVATE_HAZARDOUS_MATERIALS_TRANSPORT] = "Transporte Privado de materiales y residuos peligrosos.",
            [PermisoSct.INTERNATIONAL_LONG_HAUL_TRANSPORT] = "Autotransporte internacional de carga de largo recorrido.",
            [PermisoSct.INTERNATIONAL_SPECIALIZED_HAZARDOUS_LONG_HAUL] = "Autotransporte internacional de carga especializada de materiales y residuos peligrosos de largo recorrido.",
            [PermisoSct.FEDERAL_TRANSPORT_US_BORDER_ZONE] = "Autotransporte Federal de Carga General cuyo ámbito de aplicación comprende la franja fronteriza con Estados Unidos.",
            [PermisoSct.FEDERAL_SPECIALIZED_US_BORDER_ZONE] = "Autotransporte Federal de Carga Especializada cuyo ámbito de aplicación comprende la franja fronteriza con Estados Unidos.",
            [PermisoSct.AUXILIARY_TOWING_SERVICE] = "Servicio auxiliar de arrastre en las vías generales de comunicación.",
            [PermisoSct.AUXILIARY_TOWING_AND_STORAGE_SERVICE] = "Servicio auxiliar de servicios de arrastre, arrastre y salvamento, y depósito de vehículos en las vías generales de comunicación.",
            [PermisoSct.PACKAGING_AND_COURIER_SERVICE] = "Servicio de paquetería y mensajería en las vías generales de comunicación.",
            [PermisoSct.SPECIAL_TRANSPORT_INDUSTRIAL_CRANES_UP_TO_90_TONS] = "Transporte especial para el tránsito de grúas industriales con peso máximo de 90 toneladas.",
            [PermisoSct.FEDERAL_RENTAL_COMPANIES_SERVICE] = "Servicio federal para empresas arrendadoras servicio público federal.",
            [PermisoSct.VEHICLE_MOVERS_NEW_VEHICLES] = "Empresas trasladistas de vehículos nuevos.",
            [PermisoSct.MANUFACTURERS_DISTRIBUTORS_NEW_VEHICLES] = "Empresas fabricantes o distribuidoras de vehículos nuevos.",
            [PermisoSct.AUTHORIZATION_DOUBLE_ARTICULATED_TRUCK] = "Autorización expresa para circular en los caminos y puentes de jurisdicción federal con configuraciones de tractocamión doblemente articulado.",
            [PermisoSct.FEDERAL_SPECIALIZED_FUNDS_AND_VALUES] = "Autotransporte Federal de Carga Especializada de fondos y valores.",
            [PermisoSct.TEMPORARY_CABOTAGE_NAVIGATION] = "Permiso temporal para navegación de cabotaje",
            [PermisoSct.NATIONAL_INTERNATIONAL_REGULAR_SERVICE_MEXICAN] = "Concesión y/o autorización para el servicio regular nacional y/o internacional para empresas mexicanas",
            [PermisoSct.FOREIGN_COMPANIES_REGULAR_AIR_SERVICE] = "Permiso para el servicio aéreo regular de empresas extranjeras",
            [PermisoSct.NATIONAL_INTERNATIONAL_CHARTER_SERVICE] = "Permiso para el servicio nacional e internacional no regular de fletamento",
            [PermisoSct.NATIONAL_INTERNATIONAL_AIR_TAXI_SERVICE] = "Permiso para el servicio nacional e internacional no regular de taxi aéreo",
            [PermisoSct.NOT_IN_CATALOG] = "Permiso no contemplado en el catálogo."
        };
    }

    public static class SectorCofepris
    {
        public const string MEDICINE = "01";
        public const string PRECURSORS_AND_DUAL_USE_CHEMICALS = "02";
        public const string PSYCHOTROPIC_AND_NARCOTIC = "03";
        public const string TOXIC_SUBSTANCES = "04";
        public const string PESTICIDES_AND_FERTILIZERS = "05";
    }

    public static class SectorCofeprisDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [SectorCofepris.MEDICINE] = "Medicamento",
            [SectorCofepris.PRECURSORS_AND_DUAL_USE_CHEMICALS] = "Precursores y químicos de uso dual",
            [SectorCofepris.PSYCHOTROPIC_AND_NARCOTIC] = "Psicotrópicos y estupefacientes",
            [SectorCofepris.TOXIC_SUBSTANCES] = "Sustancias tóxicas",
            [SectorCofepris.PESTICIDES_AND_FERTILIZERS] = "Plaguicidas y fertilizantes"
        };
    }

    public static class PharmaceuticalForms
    {
        public const string TABLET = "01";
        public const string CAPSULES = "02";
        public const string COMPRESSED = "03";
        public const string SUGAR_COATED = "04";
        public const string SUSPENSION = "05";
        public const string SOLUTION = "06";
        public const string EMULSION = "07";
        public const string SYRUP = "08";
        public const string INJECTABLE = "09";
        public const string CREAM = "10";
        public const string OINTMENT = "11";
        public const string AEROSOL = "12";
        public const string MEDICINAL_GAS = "13";
        public const string GEL = "14";
        public const string IMPLANT = "15";
        public const string OVULE = "16";
        public const string PATCH = "17";
        public const string PASTE = "18";
        public const string POWDER = "19";
        public const string SUPPOSITORY = "20";
    }

    public static class PharmaceuticalFormDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [PharmaceuticalForms.TABLET] = "Tableta",
            [PharmaceuticalForms.CAPSULES] = "Cápsulas",
            [PharmaceuticalForms.COMPRESSED] = "Comprimidos",
            [PharmaceuticalForms.SUGAR_COATED] = "Grageas",
            [PharmaceuticalForms.SUSPENSION] = "Suspensión",
            [PharmaceuticalForms.SOLUTION] = "Solución",
            [PharmaceuticalForms.EMULSION] = "Emulsión",
            [PharmaceuticalForms.SYRUP] = "Jarabe",
            [PharmaceuticalForms.INJECTABLE] = "Inyectable",
            [PharmaceuticalForms.CREAM] = "Crema",
            [PharmaceuticalForms.OINTMENT] = "Ungüento",
            [PharmaceuticalForms.AEROSOL] = "Aerosol",
            [PharmaceuticalForms.MEDICINAL_GAS] = "Gas medicinal",
            [PharmaceuticalForms.GEL] = "Gel",
            [PharmaceuticalForms.IMPLANT] = "Implante",
            [PharmaceuticalForms.OVULE] = "Óvulo",
            [PharmaceuticalForms.PATCH] = "Parche",
            [PharmaceuticalForms.PASTE] = "Pasta",
            [PharmaceuticalForms.POWDER] = "Polvo",
            [PharmaceuticalForms.SUPPOSITORY] = "Supositorio"
        };
    }

    public static class SpecialConditions
    {
        public const string FROZEN = "01";
        public const string REFRIGERATED = "02";
        public const string CONTROLLED_TEMPERATURE = "03";
        public const string ROOM_TEMPERATURE = "04";
    }

    public static class SpecialConditionDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [SpecialConditions.FROZEN] = "Congelados",
            [SpecialConditions.REFRIGERATED] = "Refrigerados",
            [SpecialConditions.CONTROLLED_TEMPERATURE] = "Temperatura controlada",
            [SpecialConditions.ROOM_TEMPERATURE] = "Temperatura ambiente"
        };
    }

    public static class MaterialType
    {
        public const string RAW_MATERIAL = "01";
        public const string PROCESSED_MATERIAL = "02";
        public const string FINISHED_MATERIAL = "03";
        public const string MANUFACTURING_INDUSTRY_MATERIAL = "04";
        public const string OTHER = "05";
    }

    public static class MaterialTypeDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [MaterialType.RAW_MATERIAL] = "Materia prima",
            [MaterialType.PROCESSED_MATERIAL] = "Materia procesada",
            [MaterialType.FINISHED_MATERIAL] = "Materia terminada (producto terminado)",
            [MaterialType.MANUFACTURING_INDUSTRY_MATERIAL] = "Materia para la industria manufacturera",
            [MaterialType.OTHER] = "Otra"
        };
    }

    public static class TypeOfCustomsDocument
    {
        public const string PEDIMENT = "01";
        public const string TEMPORARY_IMPORT_AUTHORIZATION = "02";
        public const string TEMPORARY_IMPORT_AUTHORIZATION_VESSELS = "03";
        public const string TEMPORARY_IMPORT_AUTHORIZATION_MAINTENANCE = "04";
        public const string IMPORT_AUTHORIZATION_SPECIAL_VEHICLES = "05";
        public const string TEMPORARY_EXPORT_NOTICE = "06";
        public const string TRANSFER_NOTICE_IMMEX_RFE_AUTHORIZED_OPERATOR = "07";
        public const string TRANSFER_NOTICE_AUTO_PARTS_BORDER_ZONE = "08";
        public const string TEMPORARY_IMPORT_CONSTANCY_CONTAINERS = "09";
        public const string MERCHANDISE_TRANSFER_CONSTANCY = "10";
        public const string DONATION_AUTHORIZATION_FOREIGN_MERCHANDISE = "11";
        public const string ATA_CARNET = "12";
        public const string EXCHANGE_LISTS = "13";
        public const string TEMPORARY_IMPORT_PERMIT = "14";
        public const string TEMPORARY_IMPORT_PERMIT_RV = "15";
        public const string TEMPORARY_IMPORT_PERMIT_VESSELS = "16";
        public const string DONATION_REQUEST_EMERGENCIES_DISASTERS = "17";
        public const string CONSOLIDATED_NOTICE = "18";
        public const string CROSSING_NOTICE_MERCHANDISE = "19";
        public const string OTHER = "20";
    }

    public static class TypeOfCustomsDocumentDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [TypeOfCustomsDocument.PEDIMENT] = "Pedimento",
            [TypeOfCustomsDocument.TEMPORARY_IMPORT_AUTHORIZATION] = "Autorización de importación temporal",
            [TypeOfCustomsDocument.TEMPORARY_IMPORT_AUTHORIZATION_VESSELS] = "Autorización de importación temporal de embarcaciones",
            [TypeOfCustomsDocument.TEMPORARY_IMPORT_AUTHORIZATION_MAINTENANCE] = "Autorización de importación temporal de mercancías, destinadas al mantenimiento y reparación de las mercancías importadas temporalmente",
            [TypeOfCustomsDocument.IMPORT_AUTHORIZATION_SPECIAL_VEHICLES] = "Autorización para la importación de vehículos especialmente construidos o transformados, equipados con dispositivos o aparatos diversos para cumplir con contrato derivado de licitación pública",
            [TypeOfCustomsDocument.TEMPORARY_EXPORT_NOTICE] = "Aviso de exportación temporal",
            [TypeOfCustomsDocument.TRANSFER_NOTICE_IMMEX_RFE_AUTHORIZED_OPERATOR] = "Aviso de traslado de mercancías de empresas con Programa IMMEX, RFE u Operador Económico Autorizado",
            [TypeOfCustomsDocument.TRANSFER_NOTICE_AUTO_PARTS_BORDER_ZONE] = "Aviso para el traslado de autopartes ubicadas en la franja o región fronteriza a la industria terminal automotriz o manufacturera de vehículos de autotransporte en el resto del territorio nacional",
            [TypeOfCustomsDocument.TEMPORARY_IMPORT_CONSTANCY_CONTAINERS] = "Constancia de importación temporal, retorno o transferencia de contenedores",
            [TypeOfCustomsDocument.MERCHANDISE_TRANSFER_CONSTANCY] = "Constancia de transferencia de mercancías",
            [TypeOfCustomsDocument.DONATION_AUTHORIZATION_FOREIGN_MERCHANDISE] = "Autorización de donación de mercancías al Fisco Federal que se encuentren en el extranjero",
            [TypeOfCustomsDocument.ATA_CARNET] = "Cuaderno ATA",
            [TypeOfCustomsDocument.EXCHANGE_LISTS] = "Listas de intercambio",
            [TypeOfCustomsDocument.TEMPORARY_IMPORT_PERMIT] = "Permiso de Importación Temporal",
            [TypeOfCustomsDocument.TEMPORARY_IMPORT_PERMIT_RV] = "Permiso de importación temporal de casa rodante",
            [TypeOfCustomsDocument.TEMPORARY_IMPORT_PERMIT_VESSELS] = "Permiso de importación temporal de embarcaciones",
            [TypeOfCustomsDocument.DONATION_REQUEST_EMERGENCIES_DISASTERS] = "Solicitud de donación de mercancías en casos de emergencias o desastres naturales",
            [TypeOfCustomsDocument.CONSOLIDATED_NOTICE] = "Aviso de consolidado",
            [TypeOfCustomsDocument.CROSSING_NOTICE_MERCHANDISE] = "Aviso de cruce de mercancias",
            [TypeOfCustomsDocument.OTHER] = "Otro"
        };
    }

    public static class TransportType
    {
        public const string UNIT_TRUCK = "PT01";
        public const string TRUCK = "PT02";
        public const string TRACTOR_TRUCK = "PT03";
        public const string TRAILER = "PT04";
        public const string SEMI_TRAILER = "PT05";
        public const string LIGHT_LOAD_VEHICLE = "PT06";
        public const string CRANE = "PT07";
        public const string AIRCRAFT = "PT08";
        public const string SHIP_OR_VESSEL = "PT09";
        public const string CAR_OR_WAGON = "PT10";
        public const string CONTAINER = "PT11";
        public const string LOCOMOTIVE = "PT12";
    }

    public static class TransportTypeDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [TransportType.UNIT_TRUCK] = "Camión unitario",
            [TransportType.TRUCK] = "Camión",
            [TransportType.TRACTOR_TRUCK] = "Tractocamión",
            [TransportType.TRAILER] = "Remolque",
            [TransportType.SEMI_TRAILER] = "Semirremolque",
            [TransportType.LIGHT_LOAD_VEHICLE] = "Vehículo ligero de carga",
            [TransportType.CRANE] = "Grúa",
            [TransportType.AIRCRAFT] = "Aeronave",
            [TransportType.SHIP_OR_VESSEL] = "Barco o buque",
            [TransportType.CAR_OR_WAGON] = "Carro o vagón",
            [TransportType.CONTAINER] = "Contenedor",
            [TransportType.LOCOMOTIVE] = "Locomotora"
        };
    }

    public static class TransportFigure
    {
        public const string OPERATOR = "01";
        public const string OWNER = "02";
        public const string LESSOR = "03";
        public const string NOTIFIED = "04";
        public const string COORDINATED_MEMBER = "05";
    }

    public static class TransportFigureDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [TransportFigure.OPERATOR] = "Operador",
            [TransportFigure.OWNER] = "Propietario",
            [TransportFigure.LESSOR] = "Arrendador",
            [TransportFigure.NOTIFIED] = "Notificado",
            [TransportFigure.COORDINATED_MEMBER] = "Integrante de Coordinados"
        };
    }

    public static class RegistroIstmo
    {
        public const string COATZACOALCOS_I = "01";
        public const string COATZACOALCOS_II = "02";
        public const string TEXISTEPEC = "03";
        public const string SAN_JUAN_EVANGELISTA = "04";
        public const string SALINA_CRUZ = "05";
        public const string SAN_BLAS_ATEMPA = "06";
    }

    public static class RegistroIstmoDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [RegistroIstmo.COATZACOALCOS_I] = "Coatzacoalcos I",
            [RegistroIstmo.COATZACOALCOS_II] = "Coatzacoalcos II",
            [RegistroIstmo.TEXISTEPEC] = "Texistepec",
            [RegistroIstmo.SAN_JUAN_EVANGELISTA] = "San Juan Evangelista",
            [RegistroIstmo.SALINA_CRUZ] = "Salina Cruz",
            [RegistroIstmo.SAN_BLAS_ATEMPA] = "San Blas Atempa"
        };
    }

    public static class LoadingKey
    {
        public const string GENERAL_LOOSE_CARGO = "CGS";
        public const string GENERAL_CONTAINERIZED_CARGO = "CGC";
        public const string BULK_MINERAL = "GMN";
        public const string AGRICULTURAL_BULK = "GAG";
        public const string OTHER_FLUIDS = "OFL";
        public const string OIL_AND_DERIVATIVES = "PYD";
    }

    public static class LoadingKeyDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [LoadingKey.GENERAL_LOOSE_CARGO] = "Carga General Suelta",
            [LoadingKey.GENERAL_CONTAINERIZED_CARGO] = "Carga General Contenerizada",
            [LoadingKey.BULK_MINERAL] = "Gran Mineral",
            [LoadingKey.AGRICULTURAL_BULK] = "Granel Agrícola",
            [LoadingKey.OTHER_FLUIDS] = "Otros Fluidos",
            [LoadingKey.OIL_AND_DERIVATIVES] = "Petróleo y Derivados"
        };
    }

    public static class ConfigMaritima
    {
        public const string SUPPLIER = "B01";
        public const string BARGE = "B02";
        public const string BULK_CARRIER = "B03";
        public const string CONTAINER_SHIP = "B04";
        public const string DREDGER = "B05";
        public const string FISHING = "B06";
        public const string GENERAL_CARGO = "B07";
        public const string CHEMICAL_TANKER = "B08";
        public const string FERRY = "B09";
        public const string RO_RO = "B10";
        public const string RESEARCH = "B11";
        public const string TANKER = "B12";
        public const string GAS_CARRIER = "B13";
        public const string TUG = "B14";
        public const string EXTRAORDINARY_SPECIALIZATION = "B15";
    }

    public static class ConfigMaritimaDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [ConfigMaritima.SUPPLIER] = "Abastecedor",
            [ConfigMaritima.BARGE] = "Barcaza",
            [ConfigMaritima.BULK_CARRIER] = "Granelero",
            [ConfigMaritima.CONTAINER_SHIP] = "Porta Contenedor",
            [ConfigMaritima.DREDGER] = "Draga",
            [ConfigMaritima.FISHING] = "Pesquero",
            [ConfigMaritima.GENERAL_CARGO] = "Carga General",
            [ConfigMaritima.CHEMICAL_TANKER] = "Quimiqueros",
            [ConfigMaritima.FERRY] = "Transbordadores",
            [ConfigMaritima.RO_RO] = "Carga RoRo",
            [ConfigMaritima.RESEARCH] = "Investigación",
            [ConfigMaritima.TANKER] = "Tanquero",
            [ConfigMaritima.GAS_CARRIER] = "Gasero",
            [ConfigMaritima.TUG] = "Remolcador",
            [ConfigMaritima.EXTRAORDINARY_SPECIALIZATION] = "Extraordinaria especialización"
        };
    }

    public static class RailTrafficType
    {
        public const string LOCAL_TRAFFIC = "TT01";
        public const string INTERLINE_FORWARDED_TRAFFIC = "TT02";
        public const string INTERLINE_RECEIVED_TRAFFIC = "TT03";
        public const string INTERLINE_TRANSIT_TRAFFIC = "TT04";
    }

    public static class RailTrafficTypeDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [RailTrafficType.LOCAL_TRAFFIC] = "Tráfico local",
            [RailTrafficType.INTERLINE_FORWARDED_TRAFFIC] = "Tráfico interlineal remitido",
            [RailTrafficType.INTERLINE_RECEIVED_TRAFFIC] = "Tráfico interlineal recibido",
            [RailTrafficType.INTERLINE_TRANSIT_TRAFFIC] = "Tráfico interlineal en tránsito"
        };
    }

    public static class ContainerType
    {
        public const string CONTAINER_20FT = "TC01";
        public const string CONTAINER_40FT = "TC02";
        public const string CONTAINER_45FT = "TC03";
        public const string CONTAINER_48FT = "TC04";
        public const string CONTAINER_53FT = "TC05";
    }

    public static class ContainerTypeDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [ContainerType.CONTAINER_20FT] = "Contenedor de 6.1 Mts de longitud",
            [ContainerType.CONTAINER_40FT] = "Contenedor de 12.2 Mts de longitud",
            [ContainerType.CONTAINER_45FT] = "Contenedor de 13.7 Mts de longitud",
            [ContainerType.CONTAINER_48FT] = "Contenedor de 14.6 Mts de longitud",
            [ContainerType.CONTAINER_53FT] = "Contenedor de 16.1 Mts de longitud"
        };
    }

    public static class MaritimeContainerType
    {
        public const string REFRIGERATED_20FT = "CM001";
        public const string REFRIGERATED_40FT = "CM002";
        public const string STANDARD_8FT = "CM003";
        public const string STANDARD_10FT = "CM004";
        public const string STANDARD_20FT = "CM005";
        public const string STANDARD_40FT = "CM006";
        public const string OPEN_SIDE = "CM007";
        public const string ISOTANK = "CM008";
        public const string FLAT_RACKS = "CM009";
        public const string TANKER_SHIP = "CM010";
        public const string FERRY = "CM011";
        public const string TOURIST_FERRY = "CM012";
    }

    public static class MaritimeContainerTypeDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [MaritimeContainerType.REFRIGERATED_20FT] = "Contenedores refrigerados de 20FT",
            [MaritimeContainerType.REFRIGERATED_40FT] = "Contenedores refrigerados de 40FT",
            [MaritimeContainerType.STANDARD_8FT] = "Contenedores estándar de 8FT",
            [MaritimeContainerType.STANDARD_10FT] = "Contenedores estándar de 10FT",
            [MaritimeContainerType.STANDARD_20FT] = "Contenedores estándar de 20FT",
            [MaritimeContainerType.STANDARD_40FT] = "Contenedores estándar de 40FT",
            [MaritimeContainerType.OPEN_SIDE] = "Contenedores Open Side",
            [MaritimeContainerType.ISOTANK] = "Contenedor Isotanque",
            [MaritimeContainerType.FLAT_RACKS] = "Contenedor flat racks",
            [MaritimeContainerType.TANKER_SHIP] = "Buque tanque",
            [MaritimeContainerType.FERRY] = "Ferri",
            [MaritimeContainerType.TOURIST_FERRY] = "Ferri – Turístico y vacíos"
        };
    }

    public static class RailCarType
    {
        public const string BOXCAR = "TC01";
        public const string GONDOLA = "TC02";
        public const string HOPPER = "TC03";
        public const string TANK = "TC04";
        public const string INTERMODAL_PLATFORM = "TC05";
        public const string GENERAL_PURPOSE_PLATFORM = "TC06";
        public const string AUTOMOTIVE_PLATFORM = "TC07";
        public const string LOCOMOTIVE = "TC08";
        public const string SPECIAL_CAR = "TC09";
        public const string PASSENGER = "TC10";
        public const string TRACK_MAINTENANCE = "TC11";
    }

    public static class RailCarTypeDescriptions
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [RailCarType.BOXCAR] = "Furgón",
            [RailCarType.GONDOLA] = "Góndola",
            [RailCarType.HOPPER] = "Tolva",
            [RailCarType.TANK] = "Tanque",
            [RailCarType.INTERMODAL_PLATFORM] = "Plataforma Intermodal",
            [RailCarType.GENERAL_PURPOSE_PLATFORM] = "Plataforma de Uso General",
            [RailCarType.AUTOMOTIVE_PLATFORM] = "Plataforma Automotriz",
            [RailCarType.LOCOMOTIVE] = "Locomotora",
            [RailCarType.SPECIAL_CAR] = "Carro Especial",
            [RailCarType.PASSENGER] = "Pasajeros",
            [RailCarType.TRACK_MAINTENANCE] = "Mantenimiento de Vía"
        };
    }

    public static class RailServiceType
    {
        public const string RAILWAY_CARS = "TS01";
        public const string INTERMODAL_RAILWAY_CARS = "TS02";
        public const string UNIT_TRAIN_RAILWAY_CARS = "TS03";
        public const string UNIT_TRAIN_INTERMODAL = "TS04";
    }

    public static class MotivoTraslado
    {
        public const string PRIORLY_INVOICED_GOODS_SHIPMENT = "01";
        public const string RELOCATION_OF_OWN_GOODS = "02";
        public const string CONSIGNMENT_CONTRACT_GOODS_SHIPMENT = "03";
        public const string GOODS_SHIPMENT_FOR_SUBSEQUENT_SALE = "04";
        public const string THIRD_PARTY_OWNED_GOODS_SHIPMENT = "05";
        public const string OTHER = "99";
    }

    public static class MotivoTrasladoDescription
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [MotivoTraslado.PRIORLY_INVOICED_GOODS_SHIPMENT] = "Envío de mercancías facturadas con anterioridad",
            [MotivoTraslado.RELOCATION_OF_OWN_GOODS] = "Reubicación de mercancías propias",
            [MotivoTraslado.CONSIGNMENT_CONTRACT_GOODS_SHIPMENT] = "Envío de mercancías objeto de contrato de consignación",
            [MotivoTraslado.GOODS_SHIPMENT_FOR_SUBSEQUENT_SALE] = "Envío de mercancías para posterior enajenación",
            [MotivoTraslado.THIRD_PARTY_OWNED_GOODS_SHIPMENT] = "Envío de mercancías propiedad de terceros",
            [MotivoTraslado.OTHER] = "Otros"
        };
    }

    public static class Incoterm
    {
        public const string CFR = "CFR";
        public const string CIF = "CIF";
        public const string CPT = "CPT";
        public const string CIP = "CIP";
        public const string DAP = "DAP";
        public const string DDP = "DDP";
        public const string DPU = "DPU";
        public const string EXW = "EXW";
        public const string FCA = "FCA";
        public const string FAS = "FAS";
        public const string FOB = "FOB";
    }

    public static class IncotermDescription
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [Incoterm.CFR] = "Coste y flete (puerto de destino convenido).",
            [Incoterm.CIF] = "Coste, seguro y flete (puerto de destino convenido).",
            [Incoterm.CPT] = "Transporte pagado hasta (el lugar de destino convenido).",
            [Incoterm.CIP] = "Transporte y seguro pagados hasta (lugar de destino convenido).",
            [Incoterm.DAP] = "Entregada en lugar.",
            [Incoterm.DDP] = "Entregada derechos pagados (lugar de destino convenido).",
            [Incoterm.DPU] = "Entregada y descargada en lugar acordado.",
            [Incoterm.EXW] = "En fabrica (lugar convenido).",
            [Incoterm.FCA] = "Franco transportista (lugar designado).",
            [Incoterm.FAS] = "Franco al costado del buque (puerto de carga convenido).",
            [Incoterm.FOB] = "Franco a bordo (puerto de carga convenido)."
        };
    }

    public static class UnidadAduana
    {
        public const string KILO = "01";
        public const string GRAMO = "02";
        public const string METRO_LINEAL = "03";
        public const string METRO_CUADRADO = "04";
        public const string METRO_CUBICO = "05";
        public const string PIEZA = "06";
        public const string CABEZA = "07";
        public const string LITRO = "08";
        public const string PAR = "09";
        public const string KILOWATT = "10";
        public const string MILLAR = "11";
        public const string JUEGO = "12";
        public const string KILOWATT_HORA = "13";
        public const string TONELADA = "14";
        public const string BARRIL = "15";
        public const string GRAMO_NETO = "16";
        public const string DECENAS = "17";
        public const string CIENTOS = "18";
        public const string DOCENAS = "19";
        public const string CAJA = "20";
        public const string BOTELLA = "21";
        public const string CARAT = "22";
        public const string SERVICIO = "99";
    }

    public static class UnidadAduanaDescription
    {
        public static readonly IReadOnlyDictionary<string, string> VALUES = new Dictionary<string, string>
        {
            [UnidadAduana.KILO] = "KILO",
            [UnidadAduana.GRAMO] = "GRAMO",
            [UnidadAduana.METRO_LINEAL] = "METRO LINEAL",
            [UnidadAduana.METRO_CUADRADO] = "METRO CUADRADO",
            [UnidadAduana.METRO_CUBICO] = "METRO CUBICO",
            [UnidadAduana.PIEZA] = "PIEZA",
            [UnidadAduana.CABEZA] = "CABEZA",
            [UnidadAduana.LITRO] = "LITRO",
            [UnidadAduana.PAR] = "PAR",
            [UnidadAduana.KILOWATT] = "KILOWATT",
            [UnidadAduana.MILLAR] = "MILLAR",
            [UnidadAduana.JUEGO] = "JUEGO",
            [UnidadAduana.KILOWATT_HORA] = "KILOWATT/HORA",
            [UnidadAduana.TONELADA] = "TONELADA",
            [UnidadAduana.BARRIL] = "BARRIL",
            [UnidadAduana.GRAMO_NETO] = "GRAMO NETO",
            [UnidadAduana.DECENAS] = "DECENAS",
            [UnidadAduana.CIENTOS] = "CIENTOS",
            [UnidadAduana.DOCENAS] = "DOCENAS",
            [UnidadAduana.CAJA] = "CAJA",
            [UnidadAduana.BOTELLA] = "BOTELLA",
            [UnidadAduana.CARAT] = "CARAT",
            [UnidadAduana.SERVICIO] = "SERVICIO"
        };
    }
}
