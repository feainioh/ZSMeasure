using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HalconCCD
{
    /// <summary>
    /// 相机位置
    /// </summary>
    public enum CameraLocation : int
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// CCD1
        /// </summary>
        CCD1 = 11,
        /// <summary>
        /// CCD2
        /// </summary>
        CCD2 = 12
    }

    /// <summary>
    /// PLC 相机动作
    /// </summary>
    internal enum CameraAction : byte
    { 
        None,
        /// <summary>
        /// 拍Mark点
        /// </summary>
        PhotoMark,
        /// <summary>
        /// 九点校正
        /// </summary>
        NineDots,
        /// <summary>
        /// 测量
        /// </summary>
        Measure
    }
    /// <summary>
    /// PLC消息类型
    /// </summary>
    public enum MessageType_PLC : int
    {
        None,

        /// <summary>
        /// 读取拍照的多个元件
        /// </summary>
        RDS_Photo,

        /// <summary>
        /// 写入值
        /// </summary>
        WriteValue,
        /// <summary>
        /// 获取PLC的中心Y值
        /// </summary>
        GetCenterY,
    }

    /// <summary>
    /// CCD状态
    /// </summary>
    internal enum CCDStatus : byte
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,        
        /// <summary>
        /// 在线
        /// </summary>
        Online,
        /// <summary>
        /// 离线
        /// </summary>
        Offline,
        /// <summary>
        /// 开启相机异常
        /// </summary>
        OpenError,
        /// <summary>
        /// 关闭相机异常
        /// </summary>
        CloseError,
        /// <summary>
        /// 抓取异常
        /// </summary>
        GrabError,
        /// <summary>
        /// 其他异常
        /// </summary>
        OtherError
    }


    /// <summary>
    /// 坐标
    /// </summary>
    public class Axis
    {
        public double X;
        public double Y;
        public Axis() { }
        public Axis(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
