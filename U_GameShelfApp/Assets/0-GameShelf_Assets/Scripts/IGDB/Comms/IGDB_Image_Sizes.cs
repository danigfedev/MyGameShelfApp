
public class IGDB_Image_Sizes
{
    public const string cover_small = "t_cover_small"; //90 x 128 	Fit
    public const string screenshot_med = "t_screenshot_med"; //	569 x 320 	Lfill, Center gravity
    public const string cover_big = "t_cover_big"; //264 x 374 	Fit
    public const string logo_med = "t_logo_med"; //284 x 160 	Fit
    public const string screenshot_big = "t_screenshot_big"; //889 x 500 	Lfill, Center gravity
    public const string screenshot_huge = "t_screenshot_huge"; //1280 x 720 	Lfill, Center gravity
    public const string thumb = "t_thumb";//    90 x 90 	Thumb, Center gravity
    public const string micro = "t_micro"; //   35 x 35 	Thumb, Center gravity
    public const string _720p  ="t_720p"; // 1280 x 720 	Fit, Center gravity
    public const string _1080p = "t_1080p"; // 1920 x 1080 	Fit, Center gravity
}

public struct cover_small_ImgSize
{
    public const string size = "t_cover_small";
    public const int width = 90;
    public const int height = 128;
}

public struct screenshot_med_ImgSize
{
    public const string size = "t_screenshot_med";
    public const int width = 569;
    public const int height = 320;
}

public struct cover_big_ImgSize
{
    public const string size = "t_cover_big";
    public const int width = 264;
    public const int height = 374;
}

public struct logo_med_ImgSize
{
    public const string size = "t_logo_med";
    public const int width = 284;
    public const int height = 160;
}

public struct screenshot_big_ImgSize
{
    public const string size = "t_screenshot_big";
    public const int width = 889;
    public const int height = 500;
}

public struct screenshot_huge_ImgSize
{
    public const string size = "t_screenshot_huge";
    public const int width = 1280;
    public const int height = 720;
}

public struct thumb_ImgSize
{
    public const string size = "t_thumb";
    public const int width = 90;
    public const int height = 90;
}

public struct micro_ImgSize
{
    public const string size = "t_micro";
    public const int width = 35;
    public const int height = 35;
}

public struct _720p_ImgSize
{
    public const string size = "t_720p";
    public const int width = 1280;
    public const int height = 720;
}

public struct _1080p_ImgSize
{
    public const string size = "t_1080p";
    public const int width = 1920;
    public const int height = 1080;
}