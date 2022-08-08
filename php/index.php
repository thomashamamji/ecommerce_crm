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
                $sql = "SELECT p.nom, p.description, p.prix, produit.addedAt, categorie.nom as nomCategorie FROM produit join categorie on (categorie.Id_categorie=produit.Id_categorie)";
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
                    echo "<div><label><strong>Nom</strong></label><br>" . $data[$i]["nom"] . "</div>";
                    echo "<div><label><strong>Description</strong></label><br>" . $data[$i]["description"] . "</div>";
                    echo "<div><label><strong>Prix</strong></label><br>" . $data[$i]["prix"] . "</div>";
                    echo "<div><label><strong>Catégorie</strong></label><br>" . $data[$i]["categorieName"] . "</div>";
                    echo "<div><label><strong>Date d'ajout</strong></label><br>" . $data[$i]["addedAt"] . "</div>";
                    echo "<div><a href='/ecommerce/acheter.php?id=" . $data[$i]["Id_produit"] . "'><button>Achat immédiat</button><br></div>";
                    echo "</div><br><br>";
                }
            ?>
        </div>
    </body>
</html>