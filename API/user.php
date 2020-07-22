<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
 
// query products
$user = isset($_GET['user']) ? $_GET['user'] : die();
$password = isset($_GET['password']) ? $_GET['password'] : die();

if ($user == 'dragon') {}
