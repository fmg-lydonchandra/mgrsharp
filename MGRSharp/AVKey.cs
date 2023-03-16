/*
 * Copyright (C) 2011 United States Government as represented by the Administrator of the
 * National Aeronautics and Space Administration.
 * All Rights Reserved.
 */

/**
 * @author Tom Gaskins
 * @version $Id$
 */

/**
 * Ported to C# from the Java port.
 *
 * @author Jussi Lahdenniemi
 */


namespace Worldwind
{
    public static class AVKey
    {
        // NOTE: Keep all keys in alphabetical order except where noted

        // Direction constants
        public static readonly string NORTHWEST = "gov.nasa.worldwind.layers.ViewControlsLayer.NorthWest";
        public static readonly string SOUTHWEST = "gov.nasa.worldwind.layers.ViewControlsLayer.SouthWest";
        public static readonly string NORTHEAST = "gov.nasa.worldwind.layers.ViewControlsLayer.NorthEast";
        public static readonly string SOUTHEAST = "gov.nasa.worldwind.layers.ViewControlsLayer.SouthEast";

        // Start alphabetic order
        public static readonly string ABOVE_GROUND_LEVEL = "gov.nasa.worldwind.avkey.AboveGroundLevel";
        public static readonly string ABOVE_GROUND_REFERENCE = "gov.nasa.worldwind.avkey.AboveGroundReference";
        public static readonly string ABOVE_MEAN_SEA_LEVEL = "gov.nasa.worldwind.avkey.AboveMeanSeaLevel";
        public static readonly string ACTION = "gov.nasa.worldwind.avkey.Action";
        public static readonly string AIRSPACE_GEOMETRY_CACHE_SIZE = "gov.nasa.worldwind.avkey.AirspaceGeometryCacheSize";
        public static readonly string ALLOW = "gov.nasa.worldwind.avkey.Allow";
        public static readonly string AUTH_TOKEN = "gov.nasa.worldwind.avkey.AuthToken";

        public static readonly string AVAILABLE_IMAGE_FORMATS = "gov.nasa.worldwind.avkey.AvailableImageFormats";
        public static readonly string AVERAGE_TILE_SIZE = "gov.nasa.worldwind.avkey.AverageTileSize";

        public static readonly string BALLOON = "gov.nasa.worldwind.avkey.Balloon";
        public static readonly string BALLOON_TEXT = "gov.nasa.worldwind.avkey.BalloonText";
        public static readonly string BACK = "gov.nasa.worldwind.avkey.Back";
        public static readonly string BEGIN = "gov.nasa.worldwind.avkey.Begin";
        public static readonly string BIG_ENDIAN = "gov.nasa.worldwind.avkey.BigEndian";
        public static readonly string BOTTOM = "gov.nasa.worldwind.avkey.Bottom";
        public static readonly string BYTE_ORDER = "gov.nasa.worldwind.avkey.ByteOrder";
        public static readonly string BANDS_ORDER = "gov.nasa.worldwind.avkey.BandsOrder";

        public static readonly string BLACK_GAPS_DETECTION = "gov.nasa.worldwind.avkey.DetectBlackGaps";
        public static readonly string BOUNDS = "gov.nasa.worldwind.avkey.Bounds";

        public static readonly string CACHE_CONTENT_TYPES = "gov.nasa.worldwind.avkey.CacheContentTypes";
        public static readonly string CENTER = "gov.nasa.worldwind.avkey.Center";

        public static readonly string CLASS_LEVEL = "gov.nasa.worldwind.avkey.ClassLevel";
        public static readonly string CLASS_LEVEL_UNCLASSIFIED = "gov.nasa.worldwind.avkey.ClassLevel.Unclassified";
        public static readonly string CLASS_LEVEL_RESTRICTED = "gov.nasa.worldwind.avkey.ClassLevel.Restricted";
        public static readonly string CLASS_LEVEL_CONFIDENTIAL = "gov.nasa.worldwind.avkey.ClassLevel.Confidential";
        public static readonly string CLASS_LEVEL_SECRET = "gov.nasa.worldwind.avkey.ClassLevel.Secret";
        public static readonly string CLASS_LEVEL_TOPSECRET = "gov.nasa.worldwind.avkey.ClassLevel.TopSecret";

        public static readonly string CLOCKWISE = "gov.nasa.worldwind.avkey.ClockWise";
        public static readonly string CLOSE = "gov.nasa.worldwind.avkey.Close";
        public static readonly string COLOR = "gov.nasa.worldwind.avkey.Color";
        public static readonly string COMPRESS_TEXTURES = "gov.nasa.worldwind.avkey.CompressTextures";
        public static readonly string CONSTRUCTION_PARAMETERS = "gov.nasa.worldwind.avkey.ConstructionParameters";
        public static readonly string CONTEXT = "gov.nasa.worldwind.avkey.Context";
        public static readonly string COORDINATE_SYSTEM = "gov.nasa.worldwind.avkey.CoordinateSystem";
        public static readonly string COORDINATE_SYSTEM_GEOGRAPHIC = "gov.nasa.worldwind.avkey.CoordinateSystem.Geographic";
        public static readonly string COORDINATE_SYSTEM_NAME = "gov.nasa.worldwind.avkey.CoordinateSystem.Name";
        public static readonly string COORDINATE_SYSTEM_PROJECTED = "gov.nasa.worldwind.avkey.CoordinateSystem.Projected";
        public static readonly string COORDINATE_SYSTEM_SCREEN = "gov.nasa.worldwind.avkey.CoordinateSystem.Screen";
        public static readonly string COORDINATE_SYSTEM_UNKNOWN = "gov.nasa.worldwind.avkey.CoordinateSystem.Unknown";

        public static readonly string COUNTER_CLOCKWISE = "gov.nasa.worldwind.avkey.CounterClockWise";

        public static readonly string DATABASE_CONNECTION_POOL_CLASS_NAME = "gov.nasa.worldwind.avkey.Database.ConnectionPoolClassName";
        public static readonly string DATABASE_CONNECTION_POOL_FACTORY_CLASS_NAME
        = "gov.nasa.worldwind.avkey.Database.ConnectionPoolFactoryClassName";
        public static readonly string DATABASE_CONNECTION_STRING = "gov.nasa.worldwind.avkey.Database.ConnectionString";
        public static readonly string DATABASE_DRIVER_CLASS_NAME = "gov.nasa.worldwind.avkey.Database.DriverClassName";
        public static readonly string DATABASE_PASSWORD = "gov.nasa.worldwind.avkey.Database.Password";
        public static readonly string DATABASE_POLICY_INITIAL_CONNECTIONS = "gov.nasa.worldwind.avkey.DatabasePolicy.InitialConnections";
        public static readonly string DATABASE_POLICY_MAXIMUM_CONNECTIONS = "gov.nasa.worldwind.avkey.DatabasePolicy.MaximumConnections";
        public static readonly string DATABASE_POLICY_WAIT_IF_BUSY = "gov.nasa.worldwind.avkey.DatabasePolicy.WaitIfBusy";
        public static readonly string DATABASE_USERNAME = "gov.nasa.worldwind.avkey.Database.Username";
        public static readonly string DATA_CACHE_NAME = "gov.nasa.worldwind.avkey.DataCacheNameKey";
        public static readonly string DATA_FILE_STORE_CLASS_NAME = "gov.nasa.worldwind.avkey.DataFileStoreClassName";
        public static readonly string DATA_FILE_STORE_CONFIGURATION_FILE_NAME
        = "gov.nasa.worldwind.avkey.DataFileStoreConfigurationFileName";
        public static readonly string DATASET_NAME = "gov.nasa.worldwind.avkey.DatasetNameKey";
        public static readonly string DATA_RASTER_READER_FACTORY_CLASS_NAME = "gov.nasa.worldwind.avkey.DataRasterReaderFactoryClassName";
        public static readonly string DATASET_TYPE = "gov.nasa.worldwind.avkey.DatasetTypeKey";
        public static readonly string DATE_TIME = "gov.nasa.worldwind.avkey.DateTime";
        /**
         * Indicates the primitive data type of a dataset or a buffer of data. When used as a key, the corresponding value
         * may be one of the following: <code>INT8</code>, <code>INT16</code>, <code>INT32</code>, <code>INT64</code>,
         * <code>FLOAT32</code>, or <code>FLOAT64</code>.
         */
        public static readonly string DATA_TYPE = "gov.nasa.worldwind.avkey.DataType";
        public static readonly string DELETE_CACHE_ON_EXIT = "gov.nasa.worldwind.avkey.DeleteCacheOnExit";
        public static readonly string DESCRIPTION = "gov.nasa.worldwind.avkey.Description";
        public static readonly string DETAIL_HINT = "gov.nasa.worldwind.avkey.DetailHint";
        public static readonly string DISPLAY_ICON = "gov.nasa.worldwind.avkey.DisplayIcon";
        public static readonly string DISPLAY_NAME = "gov.nasa.worldwind.avkey.DisplayName";

        public static readonly string DTED_LEVEL = "gov.nasa.worldwind.avkey.DTED.Level";

        public static readonly string EARTH_ELEVATION_MODEL_CAPABILITIES = "gov.nasa.worldwind.avkey.EarthElevationModelCapabilities";
        public static readonly string EARTH_ELEVATION_MODEL_CLASS_NAME = "gov.nasa.worldwind.avkey.EarthElevationModelClassName";
        public static readonly string EARTH_ELEVATION_MODEL_CONFIG_FILE = "gov.nasa.worldwind.avkey.EarthElevationModelConfigFile";
        public static readonly string EAST = "gov.nasa.worldwind.avkey.East";

        public static readonly string ELEVATION = "gov.nasa.worldwind.avkey.Elevation";
        public static readonly string ELEVATION_EXTREMES_FILE = "gov.nasa.worldwind.avkey.ElevationExtremesFileKey";
        public static readonly string ELEVATION_EXTREMES_LOOKUP_CACHE_SIZE = "gov.nasa.worldwind.avkey.ElevationExtremesLookupCacheSize";
        public static readonly string ELEVATION_MIN = "gov.nasa.worldwind.avkey.ElevationMinKey";
        public static readonly string ELEVATION_MAX = "gov.nasa.worldwind.avkey.ElevationMaxKey";
        public static readonly string ELEVATION_MODEL = "gov.nasa.worldwind.avkey.ElevationModel";
        public static readonly string ELEVATION_MODEL_FACTORY = "gov.nasa.worldwind.avkey.ElevationModelFactory";
        public static readonly string ELEVATION_TILE_CACHE_SIZE = "gov.nasa.worldwind.avkey.ElevationTileCacheSize";
        public static readonly string ELEVATION_UNIT = "gov.nasa.worldwind.avkey.ElevationUnit";

        public static readonly string END = "gov.nasa.worldwind.avkey.End";

        public static readonly string EXPIRY_TIME = "gov.nasa.worldwind.avkey.ExpiryTime";
        public static readonly string EXTENT = "gov.nasa.worldwind.avkey.Extent";
        public static readonly string EXTERNAL_LINK = "gov.nasa.worldwind.avkey.ExternalLink";

        public static readonly string FEEDBACK_ENABLED = "gov.nasa.worldwind.avkey.FeedbackEnabled";
        public static readonly string FEEDBACK_REFERENCE_POINT = "gov.nasa.worldwind.avkey.FeedbackReferencePoint";
        public static readonly string FEEDBACK_SCREEN_BOUNDS = "gov.nasa.worldwind.avkey.FeedbackScreenBounds";
        public static readonly string FILE = "gov.nasa.worldwind.avkey.File";
        public static readonly string FILE_NAME = "gov.nasa.worldwind.avkey.FileName";
        public static readonly string FILE_SIZE = "gov.nasa.worldwind.avkey.FileSize";
        public static readonly string FILE_STORE = "gov.nasa.worldwind.avkey.FileStore";
        public static readonly string FILE_STORE_LOCATION = "gov.nasa.worldwind.avkey.FileStoreLocation";
        public static readonly string FLOAT32 = "gov.nasa.worldwind.avkey.Float32";
        public static readonly string FLOAT64 = "gov.nasa.worldwind.avkey.Float64";
        public static readonly string FORMAT_SUFFIX = "gov.nasa.worldwind.avkey.FormatSuffixKey";
        public static readonly string FORWARD = "gov.nasa.worldwind.avkey.Forward";
        public static readonly string FOV = "gov.nasa.worldwind.avkey.FieldOfView";
        public static readonly string FORCE_LEVEL_ZERO_LOADS = "gov.nasa.worldwind.avkey.ForceLevelZeroLoads";
        public static readonly string FRACTION = "gov.nasa.worldwind.avkey.Fraction";
        public static readonly string FRAME_TIMESTAMP = "gov.nasa.worldwind.avkey.FrameTimestamp";

        public static readonly string GDAL_AREA = "gov.nasa.worldwind.avkey.GDAL.Area";
        public static readonly string GDAL_CACHEMAX = "gov.nasa.worldwind.avkey.GDAL.CacheMax";
        public static readonly string GDAL_DEBUG = "gov.nasa.worldwind.avkey.GDAL.Debug";
        public static readonly string GDAL_MASK_DATASET = "gov.nasa.worldwind.avkey.GDAL.MaskDataset";
        public static readonly string GDAL_TIMEOUT = "gov.nasa.worldwind.avkey.GDAL.TimeOut";
        public static readonly string GDAL_PATH = "gov.nasa.worldwind.avkey.GDAL.Path";

        public static readonly string GET_CAPABILITIES_URL = "gov.nasa.worldwind.avkey.GetCapabilitiesURL";
        public static readonly string GET_MAP_URL = "gov.nasa.worldwind.avkey.GetMapURL";
        public static readonly string GLOBE = "gov.nasa.worldwind.avkey.GlobeObject";
        public static readonly string GLOBE_CLASS_NAME = "gov.nasa.worldwind.avkey.GlobeClassName";
        public static readonly string GRAYSCALE = "gov.nasa.worldwind.avkey.Grayscale";
        public static readonly string GREAT_CIRCLE = "gov.nasa.worldwind.avkey.GreatCircle";

        public static readonly string HEIGHT = "gov.nasa.worldwind.avkey.Height";
        public static readonly string HIDDEN = "gov.nasa.worldwind.avkey.Hidden";
        public static readonly string HORIZONTAL = "gov.nasa.worldwind.avkey.Horizontal";
        public static readonly string HOT_SPOT = "gov.nasa.worldwind.avkey.HotSpot";
        public static readonly string HOVER_TEXT = "gov.nasa.worldwind.avkey.HoverText";
        public static readonly string HTTP_FILE_UPLOAD_AUTH_TOKEN = "gov.nasa.worldwind.avkey.HTTP.FileUpload.AuthToken";
        public static readonly string HTTP_FILE_UPLOAD_LOCATION = "gov.nasa.worldwind.avkey.HTTP.FileUpload.TempLocation";
        public static readonly string HTTP_FILE_UPLOAD_TEMP_LOCATION = "gov.nasa.worldwind.avkey.HTTP.FileUpload.TempLocation";
        public static readonly string HTTP_SERVER_ALLOW_FILE_UPLOAD = "gov.nasa.worldwind.avkey.HTTP.Server.Allow.FileUpload";
        public static readonly string HTTP_SERVER_ALLOW_GET = "gov.nasa.worldwind.avkey.HTTP.Server.Allow.GET";
        public static readonly string HTTP_SERVER_ALLOW_POST = "gov.nasa.worldwind.avkey.HTTP.Server.Allow.POST";
        public static readonly string HTTP_SERVER_FILE_UPLOAD_MAX_SIZE = "gov.nasa.worldwind.avkey.HTTP.Server.FileUpload.MaxSize";

        public static readonly string ICON_NAME = "gov.nasa.worldwind.avkey.IconName";
        public static readonly string IGNORE = "gov.nasa.worldwind.avkey.Ignore";
        public static readonly string IMAGE = "gov.nasa.worldwind.avkey.Image";
        public static readonly string IMAGE_FORMAT = "gov.nasa.worldwind.avkey.ImageFormat";
        /**
         * Indicates whether an image represents color or grayscale values. When used as a key, the corresponding value may
         * be one of the following: <code>COLOR</code> or <code>GRAYSCALE</code>.
         */
        public static readonly string IMAGE_COLOR_FORMAT = "gov.nasa.worldwind.avkey.ImageColorFormat";
        public static readonly string INACTIVE_LEVELS = "gov.nasa.worldwind.avkey.InactiveLevels";
        public static readonly string INSTALLED = "gov.nasa.worldwind.avkey.Installed";
        public static readonly string INITIAL_ALTITUDE = "gov.nasa.worldwind.avkey.InitialAltitude";
        public static readonly string INITIAL_HEADING = "gov.nasa.worldwind.avkey.InitialHeading";
        public static readonly string INITIAL_LATITUDE = "gov.nasa.worldwind.avkey.InitialLatitude";
        public static readonly string INITIAL_LONGITUDE = "gov.nasa.worldwind.avkey.InitialLongitude";
        public static readonly string INITIAL_PITCH = "gov.nasa.worldwind.avkey.InitialPitch";
        public static readonly string INPUT_HANDLER_CLASS_NAME = "gov.nasa.worldwind.avkey.InputHandlerClassName";
        public static readonly string INSET_PIXELS = "gov.nasa.worldwind.avkey.InsetPixels";
        public static readonly string INT8 = "gov.nasa.worldwind.avkey.Int8";
        public static readonly string INT16 = "gov.nasa.worldwind.avkey.Int16";
        public static readonly string INT32 = "gov.nasa.worldwind.avkey.Int32";
        public static readonly string INT64 = "gov.nasa.worldwind.avkey.Int64";

        public static readonly string KEYSTORE_PATH = "gov.nasa.worldwind.avkey.KeystorePath";
        public static readonly string KEYSTORE_USER = "gov.nasa.worldwind.avkey.KeystoreUser";
        public static readonly string KEYSTORE_PASSWORD = "gov.nasa.worldwind.avkey.KeystorePassword";

        public static readonly string LAST_UPDATE = "gov.nasa.worldwind.avkey.LastUpdateKey";
        public static readonly string LAYER = "gov.nasa.worldwind.avkey.LayerObject";
        public static readonly string LAYER_ABSTRACT = "gov.nasa.worldwind.avkey.LayerAbstract";
        public static readonly string LAYER_DESCRIPTOR_FILE = "gov.nasa.worldwind.avkey.LayerDescriptorFile";
        public static readonly string LAYER_FACTORY = "gov.nasa.worldwind.avkey.LayerFactory";
        public static readonly string LAYER_NAME = "gov.nasa.worldwind.avkey.LayerName";
        public static readonly string LAYER_NAMES = "gov.nasa.worldwind.avkey.LayerNames";
        public static readonly string LAYERS = "gov.nasa.worldwind.avkey.LayersObject";
        public static readonly string LAYERS_CLASS_NAMES = "gov.nasa.worldwind.avkey.LayerClassNames";
        public static readonly string LEFT = "gov.nasa.worldwind.avkey.Left";
        public static readonly string LEFT_OF_CENTER = "gov.nasa.worldwind.avkey.LeftOfCenter";
        public static readonly string LEVEL_NAME = "gov.nasa.worldwind.avkey.LevelNameKey";
        public static readonly string LEVEL_NUMBER = "gov.nasa.worldwind.avkey.LevelNumberKey";
        public static readonly string LEVEL_ZERO_TILE_DELTA = "gov.nasa.worldwind.avkey.LevelZeroTileDelta";
        public static readonly string LINEAR = "gov.nasa.worldwind.avkey.Linear";
        public static readonly string LITTLE_ENDIAN = "gov.nasa.worldwind.avkey.LittleEndian";
        public static readonly string LOGGER_NAME = "gov.nasa.worldwind.avkey.LoggerName";
        public static readonly string LOXODROME = "gov.nasa.worldwind.avkey.Loxodrome";

        public static readonly string MAP_SCALE = "gov.nasa.worldwind.avkey.MapScale";
        public static readonly string MARS_ELEVATION_MODEL_CLASS_NAME = "gov.nasa.worldwind.avkey.MarsElevationModelClassName";
        public static readonly string MARS_ELEVATION_MODEL_CONFIG_FILE = "gov.nasa.worldwind.avkey.MarsElevationModelConfigFile";

        /**
         * Describes the maximum number of attempts to make when downloading a resource before attempts are suspended.
         * Attempts are restarted after the interval specified by {@link #MIN_ABSENT_TILE_CHECK_INTERVAL}.
         *
         * @see #MIN_ABSENT_TILE_CHECK_INTERVAL
         */
        public static readonly string MAX_ABSENT_TILE_ATTEMPTS = "gov.nasa.worldwind.avkey.MaxAbsentTileAttempts";

        public static readonly string MAX_ACTIVE_ALTITUDE = "gov.nasa.worldwind.avkey.MaxActiveAltitude";
        public static readonly string MAX_MESSAGE_REPEAT = "gov.nasa.worldwind.avkey.MaxMessageRepeat";
        public static readonly string MEMORY_CACHE_SET_CLASS_NAME = "gov.nasa.worldwind.avkey.MemoryCacheSetClassName";
        public static readonly string MIME_TYPE = "gov.nasa.worldwind.avkey.MimeType";

        /**
         * Describes the interval to wait before allowing further attempts to download a resource after the number of
         * attempts specified by {@link #MAX_ABSENT_TILE_ATTEMPTS} are made.
         *
         * @see #MAX_ABSENT_TILE_ATTEMPTS
         */
        public static readonly string MIN_ABSENT_TILE_CHECK_INTERVAL = "gov.nasa.worldwind.avkey.MinAbsentTileCheckInterval";
        public static readonly string MIN_ACTIVE_ALTITUDE = "gov.nasa.worldwind.avkey.MinActiveAltitude";

        // Implementation note: the keys MISSING_DATA_SIGNAL and MISSING_DATA_REPLACEMENT are intentionally different than
        // their actual string values. Legacy code is expecting the string values "MissingDataFlag" and "MissingDataValue",
        // respectively.
        public static readonly string MISSING_DATA_SIGNAL = "gov.nasa.worldwind.avkey.MissingDataFlag";
        public static readonly string MISSING_DATA_REPLACEMENT = "gov.nasa.worldwind.avkey.MissingDataValue";

        public static readonly string MODEL = "gov.nasa.worldwind.avkey.ModelObject";
        public static readonly string MODEL_CLASS_NAME = "gov.nasa.worldwind.avkey.ModelClassName";
        public static readonly string MOON_ELEVATION_MODEL_CLASS_NAME = "gov.nasa.worldwind.avkey.MoonElevationModelClassName";
        public static readonly string MOON_ELEVATION_MODEL_CONFIG_FILE = "gov.nasa.worldwind.avkey.MoonElevationModelConfigFile";

        public static readonly string NAME = "gov.nasa.worldwind.avkey.Name";
        public static readonly string NETWORK_STATUS_CLASS_NAME = "gov.nasa.worldwind.avkey.NetworkStatusClassName";
        public static readonly string NETWORK_STATUS_TEST_SITES = "gov.nasa.worldwind.avkey.NetworkStatusTestSites";
        public static readonly string NEXT = "gov.nasa.worldwind.avkey.Next";
        public static readonly string NUM_BANDS = "gov.nasa.worldwind.avkey.NumBands";
        public static readonly string NUM_EMPTY_LEVELS = "gov.nasa.worldwind.avkey.NumEmptyLevels";
        public static readonly string NUM_LEVELS = "gov.nasa.worldwind.avkey.NumLevels";
        public static readonly string NETWORK_RETRIEVAL_ENABLED = "gov.nasa.worldwind.avkey.NetworkRetrievalEnabled";
        public static readonly string NORTH = "gov.nasa.worldwind.avkey.North";

        public static readonly string OFFLINE_MODE = "gov.nasa.worldwind.avkey.OfflineMode";
        public static readonly string OPACITY = "gov.nasa.worldwind.avkey.Opacity";
        /**
         * Indicates an object's position in a series. When used as a key, the corresponding value must be an {@link
         * Integer} object indicating the ordinal.
         */
        public static readonly string ORDINAL = "gov.nasa.worldwind.avkey.Ordinal";
        /**
         * Indicates a list of one or more object's positions in a series. When used as a key, the corresponding value must
         * be a {@link java.util.List} of {@link Integer} objects indicating the ordinals.
         */
        public static readonly string ORDINAL_LIST = "gov.nasa.worldwind.avkey.OrdinalList";
        public static readonly string ORIGIN = "gov.nasa.worldwind.avkey.Origin";

        public static readonly string OVERVIEW_FILE_NAME = "gov.nasa.worldwind.avkey.Overview.FileName";

        public static readonly string PARENT_LAYER_NAME = "gov.nasa.worldwind.avkey.ParentLayerName";

        public static readonly string PAUSE = "gov.nasa.worldwind.avkey.Pause";
        public static readonly string PICKED_OBJECT = "gov.nasa.worldwind.avkey.PickedObject";
        public static readonly string PICKED_OBJECT_ID = "gov.nasa.worldwind.avkey.PickedObject.ID";
        public static readonly string PICKED_OBJECT_PARENT_LAYER = "gov.nasa.worldwind.avkey.PickedObject.ParentLayer";
        public static readonly string PICKED_OBJECT_PARENT_LAYER_NAME = "gov.nasa.worldwind.avkey.PickedObject.ParentLayer.Name";
        public static readonly string PICKED_OBJECT_SIZE = "gov.nasa.worldwind.avkey.PickedObject.Size";
        public static readonly string PIXELS = "gov.nasa.worldwind.avkey.Pixels";
        /**
         * Indicates whether a raster's pixel values represent imagery or elevation data. When used as a key, the
         * corresponding value may be one of the following: <code>IMAGERY</code> or <code>ELEVATION</code>.
         */
        public static readonly string PIXEL_FORMAT = "gov.nasa.worldwind.avkey.PixelFormat";
        public static readonly string PIXEL_HEIGHT = "gov.nasa.worldwind.avkey.PixelHeight";
        public static readonly string PIXEL_WIDTH = "gov.nasa.worldwind.avkey.PixelWidth";
        /** @deprecated Use <code>{@link #DATA_TYPE} instead.</code>. */
        public static readonly string PIXEL_TYPE = AVKey.DATA_TYPE;

        public static readonly string PLACENAME_LAYER_CACHE_SIZE = "gov.nasa.worldwind.avkey.PlacenameLayerCacheSize";
        public static readonly string PLAY = "gov.nasa.worldwind.avkey.Play";
        public static readonly string POSITION = "gov.nasa.worldwind.avkey.Position";
        public static readonly string PREVIOUS = "gov.nasa.worldwind.avkey.Previous";

        public static readonly string PRODUCER_ENABLE_FULL_PYRAMID = "gov.nasa.worldwind.avkey.Producer.EnableFullPyramid";

        public static readonly string PROGRESS = "gov.nasa.worldwind.avkey.Progress";
        public static readonly string PROGRESS_MESSAGE = "gov.nasa.worldwind.avkey.ProgressMessage";

        public static readonly string PROJECTION_DATUM = "gov.nasa.worldwind.avkey.Projection.Datum";
        public static readonly string PROJECTION_DESC = "gov.nasa.worldwind.avkey.Projection.Description";
        public static readonly string PROJECTION_EPSG_CODE = "gov.nasa.worldwind.avkey.Projection.EPSG.Code";
        public static readonly string PROJECTION_HEMISPHERE = "gov.nasa.worldwind.avkey.Projection.Hemisphere";
        public static readonly string PROJECTION_NAME = "gov.nasa.worldwind.avkey.Projection.Name";
        public static readonly string PROJECTION_UNITS = "gov.nasa.worldwind.avkey.Projection.Units";
        public static readonly string PROJECTION_UNKNOWN = "gov.nasa.worldwind.Projection.Unknown";
        public static readonly string PROJECTION_UTM = "gov.nasa.worldwind.avkey.Projection.UTM";
        public static readonly string PROJECTION_ZONE = "gov.nasa.worldwind.avkey.Projection.Zone";

        public static readonly string PROPERTIES = "gov.nasa.worldwind.avkey.Properties";

        public static readonly string PROTOCOL = "gov.nasa.worldwind.avkey.Protocol";
        public static readonly string PROTOCOL_HTTP = "gov.nasa.worldwind.avkey.Protocol.HTTP";
        public static readonly string PROTOCOL_HTTPS = "gov.nasa.worldwind.avkey.Protocol.HTTPS";

        public static readonly string RECTANGLES = "gov.nasa.worldwind.avkey.Rectangles";

        public static readonly string RELATIVE_TO_GLOBE = "gov.nasa.worldwind.avkey.RelativeToGlobe";
        public static readonly string RELATIVE_TO_SCREEN = "gov.nasa.worldwind.avkey.RelativeToScreen";

        public static readonly string RASTER_BAND_ACTUAL_BITS_PER_PIXEL = "gov.nasa.worldwind.avkey.RasterBand.ActualBitsPerPixel";
        public static readonly string RASTER_BAND_MIN_PIXEL_VALUE = "gov.nasa.worldwind.avkey.RasterBand.MinPixelValue";
        public static readonly string RASTER_BAND_MAX_PIXEL_VALUE = "gov.nasa.worldwind.avkey.RasterBand.MaxPixelValue";

        public static readonly string RASTER_HAS_ALPHA = "gov.nasa.worldwind.avkey.RasterHasAlpha";
        public static readonly string RASTER_HAS_OVERVIEWS = "gov.nasa.worldwind.avkey.Raster.HasOverviews";
        public static readonly string RASTER_HAS_VOIDS = "gov.nasa.worldwind.avkey.Raster.HasVoids";
        public static readonly string RASTER_LAYER_CLASS_NAME = "gov.nasa.worldwind.avkey.RasterLayer.ClassName";
        public static readonly string RASTER_PIXEL = "gov.nasa.worldwind.avkey.RasterPixel";
        public static readonly string RASTER_PIXEL_IS_AREA = "gov.nasa.worldwind.avkey.RasterPixelIsArea";
        public static readonly string RASTER_PIXEL_IS_POINT = "gov.nasa.worldwind.avkey.RasterPixelIsPoint";
        public static readonly string RECTANGULAR_TESSELLATOR_MAX_LEVEL = "gov.nasa.worldwind.avkey.RectangularTessellatorMaxLevel";
        public static readonly string REPAINT = "gov.nasa.worldwind.avkey.Repaint";
        public static readonly string REPEAT_NONE = "gov.nasa.worldwind.avkey.RepeatNone";
        public static readonly string REPEAT_X = "gov.nasa.worldwind.avkey.RepeatX";
        public static readonly string REPEAT_Y = "gov.nasa.worldwind.avkey.RepeatY";
        public static readonly string REPEAT_XY = "gov.nasa.worldwind.avkey.RepeatXY";

        public static readonly string RESIZE = "gov.nasa.worldwind.avkey.Resize";
        /** On window resize, scales the item to occupy a constant relative size of the viewport. */
        public static readonly string RESIZE_STRETCH = "gov.nasa.worldwind.CompassLayer.ResizeStretch";
        /**
         * On window resize, scales the item to occupy a constant relative size of the viewport, but not larger than the
         * item's inherent size scaled by the layer's item scale factor.
         */
        public static readonly string RESIZE_SHRINK_ONLY = "gov.nasa.worldwind.CompassLayer.ResizeShrinkOnly";
        /** Does not modify the item size when the window changes size. */
        public static readonly string RESIZE_KEEP_FIXED_SIZE = "gov.nasa.worldwind.CompassLayer.ResizeKeepFixedSize";
        public static readonly string RETAIN_LEVEL_ZERO_TILES = "gov.nasa.worldwind.avkey.RetainLevelZeroTiles";
        public static readonly string RETRIEVAL_POOL_SIZE = "gov.nasa.worldwind.avkey.RetrievalPoolSize";
        public static readonly string RETRIEVE_PROPERTIES_FROM_SERVICE = "gov.nasa.worldwind.avkey.RetrievePropertiesFromService";
        public static readonly string RETRIEVAL_QUEUE_SIZE = "gov.nasa.worldwind.avkey.RetrievalQueueSize";
        public static readonly string RETRIEVAL_QUEUE_STALE_REQUEST_LIMIT = "gov.nasa.worldwind.avkey.RetrievalStaleRequestLimit";
        public static readonly string RETRIEVAL_SERVICE_CLASS_NAME = "gov.nasa.worldwind.avkey.RetrievalServiceClassName";
        public static readonly string RETRIEVER_FACTORY_LOCAL = "gov.nasa.worldwind.avkey.RetrieverFactoryLocal";
        public static readonly string RETRIEVER_FACTORY_REMOTE = "gov.nasa.worldwind.avkey.RetrieverFactoryRemote";
        public static readonly string RETRIEVER_STATE = "gov.nasa.worldwind.avkey.RetrieverState";
        public static readonly string RETRIEVAL_STATE_ERROR = "gov.nasa.worldwind.avkey.RetrievalStateError";
        public static readonly string RETRIEVAL_STATE_SUCCESSFUL = "gov.nasa.worldwind.avkey.RetrievalStateSuccessful";
        public static readonly string RHUMB_LINE = "gov.nasa.worldwind.avkey.RhumbLine";
        public static readonly string RIGHT = "gov.nasa.worldwind.avkey.Right";
        public static readonly string RIGHT_OF_CENTER = "gov.nasa.worldwind.avkey.RightOfCenter";
        public static readonly string ROLLOVER_TEXT = "gov.nasa.worldwind.avkey.RolloverText";

        public static readonly string SCALE_HINT_MIN = "gov.nasa.worldwind.avkey.ScaleHintMin";
        public static readonly string SCALE_HINT_MAX = "gov.nasa.worldwind.avkey.ScaleHintMax";
        public static readonly string SCHEDULED_TASK_POOL_SIZE = "gov.nasa.worldwind.avkey.ScheduledTaskPoolSize";
        public static readonly string SCHEDULED_TASK_SERVICE_CLASS_NAME = "gov.nasa.worldwind.avkey.ScheduledTaskServiceClassName";
        public static readonly string SCENE_CONTROLLER = "gov.nasa.worldwind.avkey.SceneControllerObject";
        public static readonly string SCENE_CONTROLLER_CLASS_NAME = "gov.nasa.worldwind.avkey.SceneControllerClassName";
        public static readonly string SCREEN = "gov.nasa.worldwind.avkey.ScreenObject";
        public static readonly string SCREEN_CREDIT = "gov.nasa.worldwind.avkey.ScreenCredit";
        public static readonly string SCREEN_CREDIT_LINK = "gov.nasa.worldwind.avkey.ScreenCreditLink";
        public static readonly string SECTOR = "gov.nasa.worldwind.avKey.Sector";
        public static readonly string SECTOR_BOTTOM_LEFT = "gov.nasa.worldwind.avkey.Sector.BottomLeft";
        public static readonly string SECTOR_BOTTOM_RIGHT = "gov.nasa.worldwind.avkey.Sector.BottomRight";
        public static readonly string SECTOR_GEOMETRY_CACHE_SIZE = "gov.nasa.worldwind.avkey.SectorGeometryCacheSize";
        public static readonly string SECTOR_RESOLUTION_LIMITS = "gov.nasa.worldwind.avkey.SectorResolutionLimits";
        public static readonly string SECTOR_RESOLUTION_LIMIT = "gov.nasa.worldwind.avkey.SectorResolutionLimit";
        public static readonly string SECTOR_UPPER_LEFT = "gov.nasa.worldwind.avkey.Sector.UpperLeft";
        public static readonly string SECTOR_UPPER_RIGHT = "gov.nasa.worldwind.avkey.Sector.UpperRight";
        public static readonly string SENDER = "gov.nasa.worldwind.avkey.Sender";
        public static readonly string SERVER = "gov.nasa.worldwind.avkey.Server";
        public static readonly string SERVER_APP_CLASS_NAME = "gov.nasa.worldwind.avkey.Server.Application.ClassName";
        public static readonly string SERVER_APP_CONFIG_FILE = "gov.nasa.worldwind.avkey.Server.Application.ConfigFile";
        public static readonly string SERVER_APP_CTX = "gov.nasa.worldwind.avkey.Server.Application.Context";
        public static readonly string SERVER_APP_URL = "gov.nasa.worldwind.avkey.Server.Application.URL";
        public static readonly string SERVER_APPLICATION_POOL_CONNECTION_TIMEOUT
        = "gov.nasa.worldwind.avkey.Server.ApplicationPool.ConnectionTimeout";
        public static readonly string SERVER_APPLICATION_POOL_SIZE = "gov.nasa.worldwind.avkey.Server.ApplicationPool.Size";
        public static readonly string SERVER_APPLICATION_POOL_THREAD_TIMEOUT
        = "gov.nasa.worldwind.avkey.Server.ApplicationPool.ThreadTimeout";
        public static readonly string SERVER_CAPABILITIES_ONLINE_RESOURCE = "gov.nasa.worldwind.avkey.Server.Capabilities.OnlineResource";
        public static readonly string SERVER_PORT = "gov.nasa.worldwind.avkey.Server.Port";
        public static readonly string SERVER_REDIRECT_TO = "gov.nasa.worldwind.avkey.Server.RedirectTo";
        public static readonly string SERVER_TEMP_DIRECTORY = "gov.nasa.worldwind.avkey.Server.TempDirectory";
        public static readonly string SERVICE = "gov.nasa.worldwind.avkey.ServiceURLKey";
        public static readonly string SERVICE_CLASS = "gov.nasa.worldwind.avkey.ServiceClass";
        public static readonly string SERVER_CERTIFICATE = "gov.nasa.worldwind.avkey.Server.Certificate";
        public static readonly string SERVICE_NAME = "gov.nasa.worldwind.avkey.ServiceName";
        public static readonly string SERVICE_NAME_LOCAL_RASTER_SERVER = "LocalRasterServer";
        public static readonly string SERVICE_NAME_OFFLINE = "Offline";
        public static readonly string SERVER_VIRTUAL_DIRECTORY = "gov.nasa.worldwind.avkey.Server.VirtualDirectory";
        public static readonly string SESSION_CACHE_CLASS_NAME = "gov.nasa.worldwind.avkey.SessionCacheClassName";
        public static readonly string SHAPE_ATTRIBUTES = "gov.nasa.worldwind.avkey.ShapeAttributes";
        public static readonly string SHAPE_CIRCLE = "gov.nasa.worldwind.avkey.ShapeCircle";
        public static readonly string SHAPE_ELLIPSE = "gov.nasa.worldwind.avkey.ShapeEllipse";
        public static readonly string SHAPE_LINE = "gov.nasa.worldwind.avkey.ShapeLine";
        public static readonly string SHAPE_NONE = "gov.nasa.worldwind.avkey.ShapeNone";
        public static readonly string SHAPE_PATH = "gov.nasa.worldwind.avkey.ShapePath";
        public static readonly string SHAPE_POLYGON = "gov.nasa.worldwind.avkey.ShapePolygon";
        public static readonly string SHAPE_QUAD = "gov.nasa.worldwind.avkey.ShapeQuad";
        public static readonly string SHAPE_RECTANGLE = "gov.nasa.worldwind.avkey.ShapeRectangle";
        public static readonly string SHAPE_SQUARE = "gov.nasa.worldwind.avkey.ShapeSquare";
        public static readonly string SHAPE_TRIANGLE = "gov.nasa.worldwind.avkey.ShapeTriangle";
        public static readonly string SHORT_DESCRIPTION = "gov.nasa.worldwind.avkey.Server.ShortDescription";
        public static readonly string SIZE_FIT_TEXT = "gov.nasa.worldwind.avkey.SizeFitText";
        public static readonly string SIZE_FIXED = "gov.nasa.worldwind.avkey.SizeFixed";
        public static readonly string SPATIAL_REFERENCE_WKT = "gov.nasa.worldwind.avkey.SpatialReference.WKT";
        public static readonly string SOUTH = "gov.nasa.worldwdind.avkey.South";
        public static readonly string START = "gov.nasa.worldwind.avkey.Start";
        public static readonly string STEREO_FOCUS_ANGLE = "gov.nasa.worldwind.StereoFocusAngle";
        public static readonly string STEREO_INTEROCULAR_DISTANCE = "gov.nasa.worldwind.StereoFInterocularDistance";
        public static readonly string STEREO_MODE = "gov.nasa.worldwind.stereo.mode"; // lowercase to match Java property convention
        public static readonly string STEREO_MODE_DEVICE = "gov.nasa.worldwind.avkey.StereoModeDevice";
        public static readonly string STEREO_MODE_NONE = "gov.nasa.worldwind.avkey.StereoModeNone";
        public static readonly string STEREO_MODE_RED_BLUE = "gov.nasa.worldwind.avkey.StereoModeRedBlue";
        public static readonly string STEREO_TYPE = "gov.nasa.worldwind.stereo.type";
        public static readonly string STEREO_TYPE_TOED_IN = "gov.nasa.worldwind.avkey.StereoModeToedIn";
        public static readonly string STEREO_TYPE_PARALLEL = "gov.nasa.worldwind.avkey.StereoModeParallel";
        public static readonly string STOP = "gov.nasa.worldwind.avkey.Stop";
        public static readonly string STYLE_NAMES = "gov.nasa.worldwind.avkey.StyleNames";
        public static readonly string SURFACE_TILE_DRAW_CONTEXT = "gov.nasa.worldwind.avkey.SurfaceTileDrawContext";

        public static readonly string TESSELLATOR_CLASS_NAME = "gov.nasa.worldwind.avkey.TessellatorClassName";
        public static readonly string TEXTURE = "gov.nasa.worldwind.avkey.Texture";
        public static readonly string TEXTURE_CACHE_SIZE = "gov.nasa.worldwind.avkey.TextureCacheSize";
        public static readonly string TEXTURE_COORDINATES = "gov.nasa.worldwind.avkey.TextureCoordinates";
        public static readonly string TEXTURE_FORMAT = "gov.nasa.worldwind.avkey.TextureFormat";
        public static readonly string TEXTURE_IMAGE_CACHE_SIZE = "gov.nasa.worldwind.avkey.TextureTileCacheSize";
        public static readonly string TARGET = "gov.nasa.worldwind.avkey.Target";
        public static readonly string TASK_POOL_SIZE = "gov.nasa.worldwind.avkey.TaskPoolSize";
        public static readonly string TASK_QUEUE_SIZE = "gov.nasa.worldwind.avkey.TaskQueueSize";
        public static readonly string TASK_SERVICE_CLASS_NAME = "gov.nasa.worldwind.avkey.TaskServiceClassName";
        public static readonly string TEXT = "gov.nasa.worldwind.avkey.Text";
        public static readonly string TEXT_EFFECT_NONE = "gov.nasa.worldwind.avkey.TextEffectNone";
        public static readonly string TEXT_EFFECT_OUTLINE = "gov.nasa.worldwind.avkey.TextEffectOutline";
        public static readonly string TEXT_EFFECT_SHADOW = "gov.nasa.worldwind.avkey.TextEffectShadow";
        public static readonly string TILE_DELTA = "gov.nasa.worldwind.avkey.TileDeltaKey";
        public static readonly string TILE_HEIGHT = "gov.nasa.worldwind.avkey.TileHeightKey";
        public static readonly string TILE_ORIGIN = "gov.nasa.worldwind.avkey.TileOrigin";
        public static readonly string TILE_RETRIEVER = "gov.nasa.worldwind.avkey.TileRetriever";
        public static readonly string TILE_URL_BUILDER = "gov.nasa.worldwind.avkey.TileURLBuilder";
        public static readonly string TILE_WIDTH = "gov.nasa.worldwind.avkey.TileWidthKey";
        public static readonly string TILED_IMAGERY = "gov.nasa.worldwind.avkey.TiledImagery";
        public static readonly string TILED_ELEVATIONS = "gov.nasa.worldwind.avkey.TiledElevations";
        public static readonly string TILED_RASTER_PRODUCER_CACHE_SIZE = "gov.nasa.worldwind.avkey.TiledRasterProducerCacheSize";
        public static readonly string TILED_RASTER_PRODUCER_LARGE_DATASET_THRESHOLD =
            "gov.nasa.worldwind.avkey.TiledRasterProducerLargeDatasetThreshold";
        public static readonly string TILED_RASTER_PRODUCER_LIMIT_MAX_LEVEL = "gov.nasa.worldwind.avkey.TiledRasterProducer.LimitMaxLevel";
        public static readonly string TITLE = "gov.nasa.worldwind.avkey.Title";
        public static readonly string TOP = "gov.nasa.worldwind.avkey.Top";
        public static readonly string TRANSPARENCY_COLORS = "gov.nasa.worldwind.avkey.TransparencyColors";
        public static readonly string TREE = "gov.nasa.worldwind.avkey.Tree";
        public static readonly string TREE_NODE = "gov.nasa.worldwind.avkey.TreeNode";

        public static readonly string UNIT_FOOT = "gov.nasa.worldwind.avkey.Unit.Foot";
        public static readonly string UNIT_METER = "gov.nasa.worldwind.avkey.Unit.Meter";

        public static readonly string UNRESOLVED = "gov.nasa.worldwind.avkey.Unresolved";
        public static readonly string URL = "gov.nasa.worldwind.avkey.URL";
        public static readonly string URL_CONNECT_TIMEOUT = "gov.nasa.worldwind.avkey.URLConnectTimeout";
        public static readonly string URL_PROXY_HOST = "gov.nasa.worldwind.avkey.UrlProxyHost";
        public static readonly string URL_PROXY_PORT = "gov.nasa.worldwind.avkey.UrlProxyPort";
        public static readonly string URL_PROXY_TYPE = "gov.nasa.worldwind.avkey.UrlProxyType";
        public static readonly string URL_READ_TIMEOUT = "gov.nasa.worldwind.avkey.URLReadTimeout";
        public static readonly string USE_MIP_MAPS = "gov.nasa.worldwind.avkey.UseMipMaps";
        public static readonly string USE_TRANSPARENT_TEXTURES = "gov.nasa.worldwind.avkey.UseTransparentTextures";

        public static readonly string VBO_THRESHOLD = "gov.nasa.worldwind.avkey.VBOThreshold";
        public static readonly string VBO_USAGE = "gov.nasa.worldwind.avkey.VBOUsage";
        public static readonly string VERSION = "gov.nasa.worldwind.avkey.Version";
        public static readonly string VERTICAL = "gov.nasa.worldwind.avkey.Vertical";
        public static readonly string VERTICAL_EXAGGERATION = "gov.nasa.worldwind.avkey.VerticalExaggeration";
        public static readonly string VERTICAL_EXAGGERATION_UP = "gov.nasa.worldwind.avkey.VerticalExaggerationUp";
        public static readonly string VERTICAL_EXAGGERATION_DOWN = "gov.nasa.worldwind.avkey.VerticalExaggerationDown";
        public static readonly string VIEW = "gov.nasa.worldwind.avkey.ViewObject";
        public static readonly string VIEW_CLASS_NAME = "gov.nasa.worldwind.avkey.ViewClassName";
        public static readonly string VIEW_INPUT_HANDLER_CLASS_NAME = "gov.nasa.worldwind.avkey.ViewInputHandlerClassName";
        public static readonly string VIEW_QUIET = "gov.nasa.worldwind.avkey.ViewQuiet";

        // Viewing operations
        public static readonly string VIEW_OPERATION = "gov.nasa.worldwind.avkey.ViewOperation";
        public static readonly string VIEW_PAN = "gov.nasa.worldwind.avkey.Pan";
        public static readonly string VIEW_LOOK = "gov.nasa.worldwind.avkey.ControlLook";
        public static readonly string VIEW_HEADING_LEFT = "gov.nasa.worldwind.avkey.HeadingLeft";
        public static readonly string VIEW_HEADING_RIGHT = "gov.nasa.worldwind.avkey.HeadingRight";
        public static readonly string VIEW_ZOOM_IN = "gov.nasa.worldwind.avkey.ZoomIn";
        public static readonly string VIEW_ZOOM_OUT = "gov.nasa.worldwind.avkey.ZoomOut";
        public static readonly string VIEW_PITCH_UP = "gov.nasa.worldwind.avkey.PitchUp";
        public static readonly string VIEW_PITCH_DOWN = "gov.nasa.worldwind.avkey.PitchDown";
        public static readonly string VIEW_FOV_NARROW = "gov.nasa.worldwind.avkey.FovNarrow";
        public static readonly string VIEW_FOV_WIDE = "gov.nasa.worldwind.avkey.FovWide";

        public static readonly string VISIBILITY_ACTION_RELEASE = "gov.nasa.worldwind.avkey.VisibilityActionRelease";
        public static readonly string VISIBILITY_ACTION_RETAIN = "gov.nasa.worldwind.avkey.VisibilityActionRetain";

        public static readonly string WAKEUP_TIMEOUT = "gov.nasa.worldwind.avkey.WakeupTimeout";
        public static readonly string WEB_VIEW_FACTORY = "gov.nasa.worldwind.avkey.WebViewFactory";
        public static readonly string WEST = "gov.nasa.worldwind.avkey.West";
        public static readonly string WIDTH = "gov.nasa.worldwind.avkey.Width";
        public static readonly string WMS_BACKGROUND_COLOR = "gov.nasa.worldwind.avkey.BackgroundColor";

        public static readonly string CAPABILITIES_TEMPLATE_PATH = "gov.nasa.worldwind.avkey.CapabilitiesTemplatePath";

        public static readonly string WFS_URL = "gov.nasa.worldwind.avkey.WFS.URL";
        public static readonly string WMS_VERSION = "gov.nasa.worldwind.avkey.WMSVersion";
        public static readonly string WORLD_MAP_IMAGE_PATH = "gov.nasa.worldwind.avkey.WorldMapImagePath";
        public static readonly string WORLD_WIND_DOT_NET_LAYER_SET = "gov.nasa.worldwind.avkey.WorldWindDotNetLayerSet";
        public static readonly string WORLD_WIND_DOT_NET_PERMANENT_DIRECTORY = "gov.nasa.worldwind.avkey.WorldWindDotNetPermanentDirectory";
        public static readonly string WORLD_WINDOW_CLASS_NAME = "gov.nasa.worldwind.avkey.WorldWindowClassName";
    }
}
