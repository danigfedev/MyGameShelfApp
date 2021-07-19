
<?php
//echo '¡Hola ' . htmlspecialchars($_POST["nombre"]) . '!';

//source: https://reqbin.com/req/v0crmky0/rest-api-post-example

$url = "https://api.igdb.com/v4/games/";

$curl = curl_init($url);
curl_setopt($curl, CURLOPT_URL, $url);
curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);

$headers = array(
   "Accept: application/json",
   "Client-ID: {client-id}",
   "Authorization: Bearer {token}",
);


$data = <<<DATA
{
  'search "locoroco"; fields name; limit 30;'
}
DATA;

curl_setopt($curl, CURLOPT_POSTFIELDS, $data);

curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
//for debug only!
curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);

$resp = curl_exec($curl);
curl_close($curl);
var_dump($resp);




//****************** Not working. Failure in authentications.
/*
$url = 'https://api.igdb.com/v4/games/';
//$data = array('key1' => 'value1', 'key2' => 'value2');
//$data = 'search "locoroco"; fields name; limit 30;';
$data = array('search "locoroco";', 'fields name;', 'limit 30;');


// use key 'http' even if you send the request to https://...
$options = array(
    'http' => array(
        'header'  =>  	"Content-type: text/plain",
        				"Client-ID: k4wgv73l6stngu0v9yf52ufze4lemg\r\n" . 
        				"Authorization: Bearer p6rhvql4yqkj3ttetm5mruko76ep9j\r\n",
        'method'  => 'POST',
        'content' => http_build_query($data)
    )
);
//"Content-type: application/x-www-form-urlencoded\r\n"
$context  = stream_context_create($options);
$result = file_get_contents($url, false, $context);
if ($result === FALSE) 
{ 
//handle error
 }

var_dump($result);

?>
*/
