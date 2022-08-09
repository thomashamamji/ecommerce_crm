<?php
    session_start();
    $validUser = $_SESSION["login"] === true;
    if (!$validUser) header('Location: /ecommerce/connexion.php');

    include('db.php');

    if (isset($_GET['id']) && isset($_POST['sub'])) {
        if ($_POST['quantite'] < 1) echo "Veuillez entrer une quantité.";
        
        try {
            echo "PARAMS : " . $_SESSION['user_id'] . "-" . $_GET['id'] . "-" . $_GET['quantite'];
            $query = $conn->prepare("insert into vente(Id_utilisateur, Id_produit, quantite) values (:uid,:id,:quantite)");
            $query->bindValue("uid", $_SESSION['user_id'], PDO::PARAM_STR);
            $query->bindValue("id", $_GET['id'], PDO::PARAM_STR);
            $query->bindValue("quantite", $_POST['quantite'], PDO::PARAM_STR);
            $isOk = $query->execute();

            if (!$isOk) echo "Une erreur est survenue lors de l'achat de votre article.";
            else header('Location: /ecommerce/index.php');
        }

        catch (PDOException $e) {
            print "Error : " . $e->getMessage() . "<br/>";
            die();
        }
    }
?>
<html>
    <head>
        <title>E-commerce</title>
        <meta charset="utf-8" />
    </head>
    <body>
        <div class="root">
            <h1>Validation de l'achat</h1>
            <form action="#" method="post">
                <div><label><strong>Quantité</strong></label><input type='number' value='0' name='quantite'><br>
                <button name="sub" type='submit'>Valider</button>
            </form>
        </div>
    </body>
</html>