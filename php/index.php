<?php
    session_start();
    $validUser = $_SESSION["login"] === true;
    if (!$validUser) header('Location: /ecommerce/connexion.php');
?>
<html>
    <head>
        <title>E-commerce</title>
        <meta charset="utf-8" />
    </head>
    <body>
        <div class="root">
            <a href="/ecommerce/deconnexion.php">Se déconnecter</a>
            <h1>Liste de films</h1>
            <?php
                $ok=false;
                include('db.php');
                $sql = "SELECT p.Id_produit, p.nom, p.description, p.prix, p.addedAt, c.nom as nomCategorie FROM produit as p join categorie as c on (c.Id_categorie=p.Id_categorie)";
                $query = $conn->prepare($sql);
                $query->execute();
                $result = $query->setFetchMode(PDO::FETCH_ASSOC);
                $data = $query->fetchAll();
                $num_rows = count($data);

                if (!$num_rows) echo "<p>Aucun produit</p>";
                $prevCategoryName = "";

                for ($i = 0; $i < $num_rows; $i++) {
                    if ($prevCategoryName != $data[$i]["nomCategorie"]) {
                        $prevCategoryName = $data[$i]["nomCategorie"];
                        echo "<h3><strong>" . $prevCategoryName . "</strong></h3><br><br>";
                    }

                    echo "<div>";
                    echo "<div><label><strong>Nom</strong></label>&nbsp" . $data[$i]["nom"] . "</div><br>";
                    echo "<div><label><strong>Description</strong></label>&nbsp" . $data[$i]["description"] . "</div><br>";
                    echo "<div><label><strong>Prix</strong></label>&nbsp" . $data[$i]["prix"] . "</div><br>";
                    echo "<div><label><strong>Date d'ajout</strong></label>&nbsp" . $data[$i]["addedAt"] . "</div><br>";
                    echo "<div><a href='/ecommerce/acheter.php?id=" . $data[$i]["Id_produit"] . "&uid=" . $_SESSION['user_id'] . "'><button>Achat immédiat</button></a><br></div>";
                    echo "</div><br><br>";
                }
            ?>
        </div>
    </body>
</html>