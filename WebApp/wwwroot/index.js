const base_url = "https://localhost:5001/api/";

new Vue({
    el: '#app',
    data: {
        username: '',
        password: '',
        message: "Q&A",
        login_placeholder: "Enter username"
    },

    methods: {
/*        signin() {
            alert("signin")
        },*/
        
        signIn() {
            let json = JSON.stringify({
                username: this.username,
                password: this.data.password
            });
            let xhttp = new XMLHttpRequest();
            xhttp.onreadystatechange = function() {
                if (this.readyState == 4 && this.status == 200) {
                    document.getElementById("demo").innerHTML = this.responseText;
                }
            };
            xhttp.open("POST", base_url + "/signin", true);
            xhttp.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhttp.send(json);
        }
    }
});
