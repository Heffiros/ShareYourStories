export default function ({ $auth, redirect, route }) {
  // Vérifie si l'utilisateur est authentifié
  console.log(route.path)
  console.log($auth.loggedIn)
  if ($auth.loggedIn) {
    console.log("ta")
    // Vérifie si la page actuelle est différente de la page de connexion et d'inscription
    if (route.path !== '/auth/login' && route.path !== '/auth/register') {
      // Redirige vers la page de connexion
      return redirect('/auth/login')
    }
  } else {
    console.log('ti')
    return
  }
}

