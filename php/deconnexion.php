<?php
    session_start();
    unset($_SESSION["user_id"]);
    unset($_SESSION["login"]);
    header("Location:/ecommerce/connexion.php");
?>