    let openLogin = document.getElementsByClassName('open_login')
    let openSignup = document.getElementsByClassName('open_signup')
    //hide
    let modal_login = document.getElementById("loginnnn")
    let modal_signup = document.getElementById('singupppp')
    //out
    let login_close = document.getElementById('out_log')
    let signup_close = document.getElementById('out_sign')
    //click login+signup
    let login_click = document.getElementById('loginn')
    let signup_click = document.getElementById('signupp')

    modal_signup.style.display = "none";
    modal_login.style.display = "none";

    function clearmain(tmp) {

        switch (tmp) {
            case 1:
                {
                    console.log(1)
                    modal_login.style.display = "flex";
                    break;
                }
            case 2:
                {
                    modal_signup.style.display = "flex";
                    console.log(2)
                    break;
                }
            default:
                {
                    modal_signup.style.display = "none";
                    modal_login.style.display = "none";
                    console.log(3)
                    break;
                }
        }


    }

    data_name_add_login();

    function data_name_add_login() {

        var database_name = ['QLVT', 'TN_CSDLPT', 'NGANHANG'];
        add_option(database_name, "pick_option_database", "pick_database", "optionfirst");
    }
    //add xong

    //add opption ne(mang du lieu, ten class moi, the cha, the con)
    function add_option(tmp, classtmp, node_parents,node_last) {
        for (let i = 0; i < tmp.length; i++) {
            var addtmp = document.createElement('option');
            var text = document.createTextNode(tmp[i]);
            var id_parents=document.getElementById(node_parents)[0];


            addtmp.appendChild(text);
            addtmp.classList.add(classtmp);
            id_parents.appendChild(addtmp);
            const node = document.getElementById(node_last).lastElementChild;
            document.getElementById(node_parents).appendChild(node);
        }
    }

    var TK_login = document.getElementById("username");
    var MK_login = document.getElementById("pass");
    var TK_sign = document.getElementById("username_sign");
    var MK_sign = document.getElementById("pass_sign");
    var otp = document.getElementById("adminnnn");
    var notifi = document.getElementById("noti");


    var btn_sigin = document.getElementById("btn_sigin");
    var btn_login = document.getElementById("btn_login");
    var user_name = document.getElementById('user_name');
    var login_done = document.getElementById('login_done');
   
    var database_pick = ''


    function database_selec_click(database_pick_select) {
        database_pick = database_pick_select.value;
        console.log(database_pick)
    }
    function xuly_login() {
        if (TK_login.value == '' && MK_login.value == '') {
            login_done.style.height = "450px";
            notifi.innerHTML = "không đước để trống ô tài khoản, mật khẩu";
        }
        else if (TK_login.value == "") {
            login_done.style.height = "450px";
            notifi.innerHTML = "không đước để trống ô tài khoản";
        }
        else if (MK_login.value == "") {
            login_done.style.height = "450px";
            notifi.innerHTML = "không đước để trống ô mật khẩu";
        }
        else if (database_pick === 'nooo' || database_pick === '' || database_pick === 'defaultt' || database_pick == false) {
            login_done.style.height = "450px";
            notifi.innerHTML = "hãy chọn data";
        }
        else {
            login_done.style.height = "450px";
            notifi.innerHTML = "thành công";
            modal_signup.style.display = "none";
            modal_login.style.display = "none";
            btn_login.style.display = 'none';
            btn_sigin.style.display = 'none';
            user_name.innerHTML = TK_login.value;
            console.log(databased_pick);
            console.log('done')

        }
    }






