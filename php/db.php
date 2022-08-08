<?php
    $servername = "192.168.1.103";
    $username = "ecommerce_admin_app_dbuser";
    $password = "app12345$$$$$";
    $dbname = "ecommerce_admin_app_db";

    try {
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    // set the PDO error mode to exception
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    } catch(PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
    }
?> 