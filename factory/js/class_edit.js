//選單縮放箭頭
function Spin() {
    var d = document.getElementById("wrapper").className;
    var m = document.getElementById("menu-toggle");
    if (d == "d-flex") {
        m.style.transform = "rotate(180deg)";
        m.style.borderColor = "#DFDFDF";
        m.style.borderStyle = "solid";
        m.style.borderWidth = "1px 0px 1px 1px";
    }
    else {
        m.style.transform = "rotate(360deg)";
        m.style.borderColor = "#DFDFDF";
        m.style.borderStyle = "solid";
        m.style.borderWidth = "1px 1px 1px 0px";
    }
}

$(document).ready(function () {
//Tag管理
var show = document.getElementById("show");
//磨機報表
//var show1 = document.getElementById("show1");
//磨機報表裡的觀音廠
//var show2 = document.getElementById("show2");
//空壓機比功率
//var ac_power_f = document.getElementById("ac_power_f");
//取得當前頁面
var url = location.pathname;

switch (url) {
    
    //Tag選取
    case '/Tag/Tag_acct.aspx':
        show.className = "collapse show";
        //icon方向
        var x = document.getElementById("Tag_s");
        x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        //被選到的選單修改為灰背景
        x = document.getElementById("Tag_acct");
        x.className = "list-group-item list-group-item-action bg-light";
        break;
    //Tag設定
    case '/Tag/Tag_Modify.aspx':
        show.className = "collapse show";
        var x = document.getElementById("Tag_u");
        x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        x = document.getElementById("Tag_acct");
        x.className = "list-group-item list-group-item-action bg-light";
        break;
    //趨勢圖-分鐘
    case '/Tag/Tag_trend.aspx':
        show.className = "collapse show";
        var x = document.getElementById("Tag_t");
        x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        x = document.getElementById("Tag_acct");
        x.className = "list-group-item list-group-item-action bg-light";
        break;
    //趨勢圖-小時
    case '/Tag/Tag_trend_h.aspx':
        show.className = "collapse show";
        var x = document.getElementById("Tag_h");
        x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        x = document.getElementById("Tag_acct");
        x.className = "list-group-item list-group-item-action bg-light";
        break;
    
    /*
    //磨機報表 - 觀音廠
    case '/Mill/Tag_report_KY.aspx':
        show1.className = "collapse show";
        show2.className = "collapse show";
        //磨機1.2號
        if (location.search == "?M=12") {
            var x = document.getElementById("KY_12");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        }
        //磨機3.4號
        else if (location.search == "?M=34") {
            var x = document.getElementById("KY_34");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        }
        //磨機5.6號
        else
        {
            var x = document.getElementById("KY_56");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        }
        var x = document.getElementById("Tag_r");
        x.className = "list-group-item list-group-item-action bg-light";
        break;
    */
    //帳號管理
    case '/acct_mgt/acct_mgt.aspx':
        var acct_mgt = document.getElementById("acct_mgt");
        acct_mgt.className = "list-group-item list-group-item-action list-group-item-dark";
        break;

    //Tag對應表
    case '/Sys_maint/Detail.aspx':
        var show1 = document.getElementById("show1");
        show1.className = "collapse show";
        //icon方向
        var x = document.getElementById("Mill_detail");
        x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        //被選到的選單修改為灰背景
        x = document.getElementById("sys_maint");
        x.className = "list-group-item list-group-item-action bg-light";
        /*
        //頁籤
        var b = location.search.substring(12, 20);
        if (b == "Power") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Power");
            m.className = "nav-link active bg-light";
        } else if (b == "Temp") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Temp");
            m.className = "nav-link active bg-light";
        } else if (b == "Wind") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Wind");
            m.className = "nav-link active bg-light";
        } else if (b == "Fd") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Fd");
            m.className = "nav-link active bg-light";
        } else {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Quality");
            m.className = "nav-link active bg-light";
        }
        */
        break;
    // 工廠電腦狀態
    case '/Light/Factory_state.aspx':
        var show1 = document.getElementById("show1");
        show1.className = "collapse show";
        //icon方向
        var x = document.getElementById("factory_state");
        x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
        //被選到的選單修改為灰背景
        x = document.getElementById("sys_maint");
        x.className = "list-group-item list-group-item-action bg-light";
        break;

    //檢測數據
    case '/Check/Check_data.aspx':
        var check_data = document.getElementById("check_data");
        check_data.className = "list-group-item list-group-item-action list-group-item-dark";
        var m = location.search.substring(8, 9);
        if (m == "0") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M0");
            b.className = "nav-link active bg-light"
        } else if (m == "1") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M1");
            b.className = "nav-link active bg-light"
        } else if (m == "3") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M2");
            b.className = "nav-link active bg-light"
        } else if (m == "5") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M3");
            b.className = "nav-link active bg-light"
        } else {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M4");
            b.className = "nav-link active bg-light"
        }
        break;

    //檢測數據-新增數據
    case '/Check/add_data.aspx':
        var check_data = document.getElementById("check_data");
        check_data.className = "list-group-item list-group-item-action list-group-item-dark";
        break;

    //實驗數據
    case '/Check/experiment_data.aspx':
        var check_data = document.getElementById("check_lb");
        check_data.className = "list-group-item list-group-item-action list-group-item-dark";
        var m = location.search.substring(8, 9);
        if (m == "0") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M0");
            b.className = "nav-link active bg-light"
        } else if (m == "1") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M1");
            b.className = "nav-link active bg-light"
        } else if (m == "3") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M2");
            b.className = "nav-link active bg-light"
        } else if (m == "5") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M3");
            b.className = "nav-link active bg-light"
        } else {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M4");
            b.className = "nav-link active bg-light"
        }
        break;

    //新增實驗數據
    case '/Check/add_Quality.aspx':
        var check_data = document.getElementById("check_lb");
        check_data.className = "list-group-item list-group-item-action list-group-item-dark";
        break;

    //台電電量
    case '/Tpc_Power/Tpc_Power.aspx':
        var tpc_power = document.getElementById("tpc_power");
        tpc_power.className = "list-group-item list-group-item-action list-group-item-dark";
        var t = location.search.substring(8, 9);
        if (t == "D") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M1");
            b.className = "nav-link active bg-light"
        } else if(t=="M") {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M2");
            b.className = "nav-link active bg-light"
        } else {
            var b = document.getElementById("ContentPlaceHolder1_hyl_M0");
            b.className = "nav-link active bg-light"
        }
        break;

    //判斷各廠主頁
    case '/factory_index/index.aspx':
        var l = location.search;
        if (l == "?F=KY-T1HIST") {
            document.title = "觀音廠-首頁";
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=BL-T1HIST") {
            document.title = "八里廠-首頁";
            var factory = document.getElementById("factory_BL");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=QX-T1HIST") {
            document.title = "全興廠-首頁";
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=ZB-T1HIST") {
            document.title = "全興廠-首頁";
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=KH-PCC-LH") {
            document.title = "高雄廠-首頁";
            var factory = document.getElementById("factory_KH");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=LD-T1HIST") {
            document.title = "龍德廠-首頁";
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=LZ-T1HIST") {
            document.title = "利澤廠-首頁";
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=HL-T1HIST") {
            document.title = "花蓮廠-首頁";
            var factory = document.getElementById("factory_HL");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        if (l == "?F=XG-T1HIST") {
            document.title = "小港廠-首頁";
            var factory = document.getElementById("factory_XG");
            factory.className = "list-group-item list-group-item-action list-group-item-dark";
        }
        break;

    //空壓機比功率
    case '/AirComp/AirCompressor.aspx':
        //ac_power_f.className = "collapse show";
        var l = location.search.substring(0, 16);
        //觀音廠
        if (l == "?F=KY-T1HIST&M=0") {
            //這個是箭頭
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            //這個是顯示的灰色
            var x = document.getElementById("factory_KY_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－觀音廠";
        }
        if (l == "?F=KY-T1HIST&M=1") {
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KY_Air_1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠";
        }
        if (l == "?F=KY-T1HIST&M=2") {
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KY_Air_2");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠";
        }
        //八里廠
        if (l == "?F=BL-T1HIST&M=0") {
            var factory = document.getElementById("factory_BL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_BL_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－八里廠";
        }
        //全興廠
        if (l == "?F=QX-T1HIST&M=0") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠";
        }
        if (l == "?F=QX-T1HIST&M=1") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Air1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠";
        }
        if (l == "?F=QX-T1HIST&M=2") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Air2");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠";
        }

        //彰濱廠
        if (location.search == "?F=ZB-T1HIST&M=0") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－彰濱廠";
        }
        if (location.search == "?F=ZB-T1HIST&M=1") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Air_1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－彰濱廠";
        }
        if (location.search == "?F=ZB-T1HIST&M=4") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Air_4");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－彰濱廠";
        }
        //高雄廠
        if (l == "?F=KH-PCC-LH&M=0") {
            var factory = document.getElementById("factory_KH");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KH_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－高雄廠";
        }
        //龍德廠
        if (l == "?F=LD-T1HIST&M=0") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－龍德廠";
        }
        if (l == "?F=LD-T1HIST&M=1") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Air1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－龍德廠1";
        }
        if (l == "?F=LD-T1HIST&M=2") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Air2");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－龍德廠2";
        }
        //利澤廠
        if (l == "?F=LZ-T1HIST&M=0") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－利澤廠";
        }
        //花蓮廠
        if (l == "?F=HL-T1HIST&M=0") {
            var factory = document.getElementById("factory_HL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_HL_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－花蓮廠";
        }

    //空壓機比功率-小時
    case '/AirComp/AirCompressorH.aspx':
        //ac_power_f.className = "collapse show";
        var l = location.search.substring(0, 16);
        //觀音廠
        if (l == "?F=KY-T1HIST&M=0") {
            //這個是箭頭
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            //這個是顯示的灰色
            var x = document.getElementById("factory_KY_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－觀音廠";
        }
        if (l == "?F=KY-T1HIST&M=1") {
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KY_Air_1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－觀音廠1";
        }
        if (l == "?F=KY-T1HIST&M=2") {
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KY_Air_2");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－觀音廠2";
        }
        //八里廠
        if (l == "?F=BL-T1HIST&M=0") {
            var factory = document.getElementById("factory_BL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_BL_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－八里廠";
        }
        //全興廠
        if (l == "?F=QX-T1HIST&M=0") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠";
        }
        if (l == "?F=QX-T1HIST&M=1") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Air1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠1";
        }
        if (l == "?F=QX-T1HIST&M=2") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Air2");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－全興廠2";
        }
        //彰濱廠
        if (l == "?F=ZB-T1HIST&M=0") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－彰濱廠";
        }
        if (l == "?F=ZB-T1HIST&M=1") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Air_1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－彰濱廠";
        }
        if (l == "?F=ZB-T1HIST&M=4") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Air_4");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－彰濱廠";
        }
        //高雄廠
        if (l == "?F=KH-PCC-LH&M=0") {
            var factory = document.getElementById("factory_KH");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KH_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－高雄廠";
        }
        //龍德廠
        if (l == "?F=LD-T1HIST&M=0") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－龍德廠";
        }
        if (l == "?F=LD-T1HIST&M=1") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Air1");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－龍德廠1";
        }
        if (l == "?F=LD-T1HIST&M=2") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Air2");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－龍德廠2";
        }
        //利澤廠
        if (l == "?F=LZ-T1HIST&M=0") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－利澤廠";
        }
        //花蓮廠
        if (l == "?F=HL-T1HIST&M=0") {
            var factory = document.getElementById("factory_HL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_HL_Air");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "空壓機比功率－花蓮廠";
        }

        //var x = document.getElementById("ac_power");
        //x.className = "list-group-item list-group-item-action bg-light";
        break;

    //用電量-日管理
    case '/power/Power_H.aspx':
        var l = location.search.substring(0, 12);
        //觀音廠
        if (l == "?F=KY-T1HIST") {
            //這個是箭頭
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            //這個是顯示的灰色
            var x = document.getElementById("factory_KY_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－觀音廠";
        }
        //八里廠
        if (l == "?F=BL-T1HIST") {
            var factory = document.getElementById("factory_BL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_BL_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－八里廠";
        }
        //全興廠
        if (l == "?F=QX-T1HIST") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－全興廠";
        }
        //彰濱廠
        if (l == "?F=ZB-T1HIST") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－彰濱廠";
        }
        //高雄廠
        if (l == "?F=KH-PCC-LH") {
            var factory = document.getElementById("factory_KH");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KH_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－高雄廠";
        }
        //龍德廠
        if (l == "?F=LD-T1HIST") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－龍德廠";

        }
        //利澤廠
        if (l == "?F=LZ-T1HIST") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－利澤廠";
        }
        //花蓮廠
        if (l == "?F=HL-T1HIST") {
            var factory = document.getElementById("factory_HL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_HL_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－花蓮廠";
        }
        break;

    //用電量-月管理
    case '/power/Power_D.aspx':
        var l = location.search.substring(0, 12);
        //觀音廠
        if (l == "?F=KY-T1HIST") {
            //這個是箭頭
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            //這個是顯示的灰色
            var x = document.getElementById("factory_KY_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－觀音廠";
        }
        //八里廠
        if (l == "?F=BL-T1HIST") {
            var factory = document.getElementById("factory_BL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_BL_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－八里廠";
        }
        //全興廠
        if (l == "?F=QX-T1HIST") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_QX_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－全興廠";
        }
        //彰濱廠
        if (l == "?F=ZB-T1HIST") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_ZB_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－彰濱廠";
        }
        //高雄廠
        if (l == "?F=KH-PCC-LH") {
            var factory = document.getElementById("factory_KH");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KH_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－高雄廠";
        }
        //龍德廠
        if (l == "?F=LD-T1HIST") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LD_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－龍德廠";

        }
        //利澤廠
        if (l == "?F=LZ-T1HIST") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－利澤廠";
        }
        //花蓮廠
        if (l == "?F=HL-T1HIST") {
            var factory = document.getElementById("factory_HL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_HL_Power");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "用電量－花蓮廠";
        }
        break;

    //磨機日報
    case '/Mill/Mill_report.aspx':
        var l = location.search.substring(0, 12);
        var b = location.search.substring(19, 25);
        if (l == "?F=KY-T1HIST") {
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";

            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_KY_Mill_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else if (m == "3") {
                var x = document.getElementById("factory_KY_Mill_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_KY_Mill_5");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "磨機日報-觀音廠";
        }
        if (l == "?F=BL-T1HIST") {
            var factory = document.getElementById("factory_BL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_BL_Mill_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_BL_Mill_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "磨機日報-八里廠";
        }
        if (l == "?F=QX-T1HIST") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_QX_Mill_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_QX_Mill_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "磨機日報-全興廠";
        }
        if (l == "?F=ZB-T1HIST") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_ZB_Mill_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else if (m == "3") {
                var x = document.getElementById("factory_ZB_Mill_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else if (m == "5") {
                var x = document.getElementById("factory_ZB_Mill_5");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_ZB_Mill_7");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "磨機日報-彰濱廠";
        }
        if (l == "?F=KH-PCC-LH") {
            var factory = document.getElementById("factory_KH");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KH_Mill");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "磨機日報-高雄廠";
        }
        if (l == "?F=LD-T1HIST") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";

            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_LD_Mill_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_LD_Mill_2");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "磨機日報-龍德廠";
        }
        if (l == "?F=LZ-T1HIST") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_Mill");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "磨機日報-利澤廠";
        }
        if (l == "?F=HL-T1HIST") {
            var factory = document.getElementById("factory_HL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_HL_Mill");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "磨機日報-花蓮廠";
        }
        //頁籤
        if (b == "Power") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Power");
            m.className = "nav-link active bg-light";
        } else if (b == "Temp") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Temp");
            m.className = "nav-link active bg-light";
        } else if (b == "Wind") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Wind");
            m.className = "nav-link active bg-light";
        } else if (b == "Fd") {
            var m = document.getElementById("ContentPlaceHolder1_hyl_Fd");
            m.className = "nav-link active bg-light";
        } else{
            var m = document.getElementById("ContentPlaceHolder1_hyl_Quality");
            m.className = "nav-link active bg-light";
        }
        break;

    //循環提運機
    case '/Mill/Mill_cycle.aspx':
        var l = location.search.substring(0, 12);
        if (l == "?F=KY-T1HIST") {
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";

            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_KY_cycle_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else if (m == "3") {
                var x = document.getElementById("factory_KY_cycle_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_KY_cycle_5");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "循環提運機-觀音廠";
        }
        if (l == "?F=BL-T1HIST") {
            var factory = document.getElementById("factory_BL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_BL_cycle_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_BL_cycle_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "循環提運機-八里廠";
        }
        if (l == "?F=QX-T1HIST") {
            var factory = document.getElementById("factory_QX");
            factory.className = "list-group-item list-group-item-action bg-light";
            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_QX_cycle_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_QX_cycle_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "循環提運機-全興廠";
        }
        if (l == "?F=ZB-T1HIST") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_ZB_cycle_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else if (m == "3") {
                var x = document.getElementById("factory_ZB_cycle_3");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else if (m == "5") {
                var x = document.getElementById("factory_ZB_cycle_5");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_ZB_cycle_7");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "循環提運機-彰濱廠";
        }
        if (l == "?F=KH-PCC-LH") {
            var factory = document.getElementById("factory_KH");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_KH_cycle");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "磨機日報-高雄廠";
        }
        if (l == "?F=LD-T1HIST") {
            var factory = document.getElementById("factory_LD");
            factory.className = "list-group-item list-group-item-action bg-light";

            var m = location.search.substring(15, 16);
            if (m == "1") {
                var x = document.getElementById("factory_LD_cycle_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            else {
                var x = document.getElementById("factory_LD_cycle_2");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "循環提運機-龍德廠";
        }
        if (l == "?F=LZ-T1HIST") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_cycle");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "循環提運機-利澤廠";
        }
        if (l == "?F=HL-T1HIST") {
            var factory = document.getElementById("factory_HL");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_HL_cycle");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "循環提運機-花蓮廠";
        }
        break;

    //太陽能-日管理
    case '/PV/PV_H.aspx':
        var l = location.search.substring(0, 12);
        //觀音廠
        if (l == "?F=KY-T1HIST") {
            //這個是箭頭
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            //這個是顯示的灰色
            var x = document.getElementById("factory_KY_PV");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "太陽能－觀音廠";
        }
        //彰濱廠
        if (l == "?F=ZB-T1HIST") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            if (location.search.substring(15, 16) == "1") {
                var x = document.getElementById("factory_ZB_PV_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            } else {
                var x = document.getElementById("factory_ZB_PV_2");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "太陽能－彰濱廠";
        }
        //利澤廠
        if (l == "?F=LZ-T1HIST") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_PV");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "太陽能－利澤廠";
        }
        //小港廠
        if (l == "?F=XG-T1HIST") {
            var factory = document.getElementById("factory_XG");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_XG_PV");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "太陽能－小港廠";
        }
        break;

    //太陽能-月管理
    case '/PV/PV_D.aspx':
        var l = location.search.substring(0, 12);
        //觀音廠
        if (l == "?F=KY-T1HIST") {
            //這個是箭頭
            var factory = document.getElementById("factory_KY");
            factory.className = "list-group-item list-group-item-action bg-light";
            //這個是顯示的灰色
            var x = document.getElementById("factory_KY_PV");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "太陽能－觀音廠";
        }
        //彰濱廠
        if (l == "?F=ZB-T1HIST") {
            var factory = document.getElementById("factory_ZB");
            factory.className = "list-group-item list-group-item-action bg-light";
            if (location.search.substring(15, 16) == "1") {
                var x = document.getElementById("factory_ZB_PV_1");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            } else {
                var x = document.getElementById("factory_ZB_PV_2");
                x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            }
            document.title = "太陽能－彰濱廠";
        }
        //利澤廠
        if (l == "?F=LZ-T1HIST") {
            var factory = document.getElementById("factory_LZ");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_LZ_PV");
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "太陽能－利澤廠";
        }
        //小港廠
        if (l == "?F=XG-T1HIST") {
            var factory = document.getElementById("factory_XG");
            factory.className = "list-group-item list-group-item-action bg-light";
            var x = document.getElementById("factory_XG");//factory_XG_PV
            x.className = "list-group-item list-group-item-action list-group-item-dark collapsed";
            document.title = "太陽能－小港廠";
        }
        break;

    }
});