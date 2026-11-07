# Subir el proyecto a GitHub (primera entrega)

1. En https://github.com cree un repositorio **vacío** llamado `Reto-Actividades-Academicas-DW2`
   (sin README ni .gitignore; ya vienen en el proyecto). Privado, e invite a la docente (Settings -> Collaborators).
2. Abra una terminal (Git Bash, PowerShell o la terminal de Visual Studio) en la carpeta `UniCampus` y ejecute:

```
git init
git branch -M main
git add .
git commit -m "feat: estructura de tres capas y flujo de registrar actividad"
git remote add origin https://github.com/SU_USUARIO/Reto-Actividades-Academicas-DW2.git
git push -u origin main
```
3. Para mostrar historial de trabajo, haga commits pequeños (un integrante por rama si son varios):
```
git commit -am "docs: actualizar README con integrantes"
git commit -am "docs: agregar capturas de pruebas"
```
4. Si el equipo tiene varios integrantes, cada uno hace `git clone`, trabaja en una rama (`git checkout -b feat/su-aporte`) y
   crea un pull request.
5. Pegue el enlace del repositorio en el informe y en la plataforma. Verifique abriendo el enlace en una ventana de incógnito
   (si es privado, confirme que la docente fue invitada).
