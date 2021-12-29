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
            <h1>Liste de films</h1>
            <?php
                $ok=false;
                include('db.php');
                $sql = "SELECT produit.nom as nom, produit.description as description, produit.prix as prix, produit.addedAt as addedAt, categorie.nom as categorieName FROM produit join categorie on (categorie.Id_categorie=produit.Id_categorie)";
                $query = $conn->prepare($sql);
                $query->execute();
                $result = $query->setFetchMode(PDO::FETCH_ASSOC);
                $data = $query->fetchAll();
                $num_rows = count($data);

                if (!$num_rows) echo "<p>Aucun produit</p>";

                for ($i = 0; $i < $num_rows; $i++) {
                    echo "<div>";
                    echo "<div><label><strong>Nom</strong></label><br>" . $data[$i]["nom"] . "</div>";
                    echo "<div><label><strong>Description</strong></label><br>" . $data[$i]["description"] . "</div>";
                    echo "<div><label><strong>Prix</strong></label><br>" . $data[$i]["prix"] . "</div>";
                    echo "<div><label><strong>Catégorie</strong></label><br>" . $data[$i]["categorieName"] . "</div>";
                    echo "<div><label><strong>Date d'ajout</strong></label><br>" . $data[$i]["addedAt"] . "</div>";
                    echo "<div><button>Achat immédiat</button><br></div>";
                    echo "<div><button>Ajouter au panier</button></div>";
                    echo "</div><br><br>";
                }
            ?>
        </div>
    </body>
</html>