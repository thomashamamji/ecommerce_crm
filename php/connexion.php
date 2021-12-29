<?php
    session_start();
    $errorMsg = "";
    $id_field = 0;
    $username_field = $password_field = "";
    $validUser = $_SESSION["login"] === true;
    $password_field = "";

    if ($validUser) header('Location: /ecommerce/index.php');

    $ok=false;
    include('db.php');
    $errorMsg = "";

    if (isset($_POST['sub']) && isset($_POST['password'])) {
        echo $_POST['username'] . ":" . $_POST['password'];
        $query = $conn->prepare("SELECT * FROM utilisateur WHERE pseudo=:username AND password=:password");
        $query->bindValue("username", $_POST['username'], PDO::PARAM_STR);
        $query->bindValue("password", $_POST['password'], PDO::PARAM_STR);
        $query->execute();

        if ($query->rowCount() > 0) {
            $user = $query->fetch(PDO::FETCH_ASSOC);
            $validUser = $_POST["password"] == $user['password'];
            if(!$validUser) $errorMsg = "Nom d'utilisateur ou mot de passe incorrecte";
            else {
                $_SESSION["login"] = true;
                $_SESSION["user_id"] = $id_field;
                header("Location: /ecommerce/index.php"); die();
            }
        }

        else {
            $errorMsg = "Nom d'utilisateur ou mot de passe incorrecte" . $username_field;
        }
    }
?>

<!DOCTYPE html>
<html>
<head>
  <meta http-equiv="content-type" content="text/html;charset=utf-8" />
  <title>Se connecter</title>
</head>
<body>
  <form name="input" action="" method="post">
  <div class="p-2">
    <label for="username">Identifiant</label>
    <input class="form-control" type="text" value="" id="username" name="username" />
  </div>
  <div class="p-2">
    <label for="password">Mot de passe</label>
    <input class="form-control" type="password" value="" id="password" name="password" />
  </div>
  <div class="p-2">
    <div class="error"><?= $errorMsg ?></div>
      <input class="btn btn-primary" type="submit" value="Se connecter" name="sub" />
    </form>
  </div>
</body>
</html>