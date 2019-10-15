function baseDomain(){
    return "https://localhost:5001/";
}

function getQueryVariable(variable)
{
       var query = window.location.search.substring(1);
       var vars = query.split("&");
       for (var i=0;i<vars.length;i++) {
               var pair = vars[i].split("=");
               if(pair[0] == variable){return pair[1];}
       }
       return(false);
}

function logout()
{
  localStorage.removeItem("token");
  window.location.href="/src/forum/login.html";
}