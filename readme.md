# JSON to XML Converter

## Description

Ce projet permet de convertir des fichiers JSON en fichiers XML tout en offrant des fonctionnalités avancées telles que :
- La recherche dans les données JSON.
- Le filtrage des données JSON.
- Le regroupement des données JSON par un champ spécifique.
- L'exclusion de certains champs avant la conversion en XML.
- La conversion du JSON final en un fichier XML valide.

## Fonctionnalités

1. **Recherche dans le JSON** :
   - Permet de rechercher des données spécifiques dans le fichier JSON.

2. **Filtrage des données JSON** :
   - Applique des filtres pour ne conserver que les données pertinentes.

3. **Regroupement des données JSON** :
   - Regroupe les données par un champ spécifique (par exemple, `rooms`).
   - Les groupes sont structurés dans le JSON final.

4. **Exclusion de champs** :
   - Permet d'exclure certains champs avant la conversion en XML.

5. **Conversion en XML** :
   - Convertit le JSON final en un fichier XML valide.

## Installation

1. Clonez le dépôt :
   ```bash
   git clone <url-du-repo>
   cd <nom-du-repo>

2. Assurez-vous d'avoir .NET installé sur votre machine.

3. Restaurez les dépendances si nécessaire :
4. dotnet restore


## Utilisation

1. Placez votre fichier JSON dans le répertoire approprié.

2. Lancez le programme :
   - Utilisez l'icône **"Play"** dans l'interface de votre IDE (par exemple, Visual Studio ou Visual Studio Code) pour exécuter le programme.

3. Suivez les instructions dans la console :
   - Sélectionnez les champs pour regrouper les données.
   - Excluez les champs si nécessaire.
   - Le fichier XML sera généré dans le répertoire de sortie.
### Description des fichiers principaux

- **JsonToXmlConverter.cs** : Gère le flux principal de la conversion JSON → XML.
- **GroupJson.cs** : Regroupe les données JSON par un champ spécifique.
- **FilterJsonData.cs** : Filtre les données JSON pour ne conserver que les éléments pertinents.
- **JsonSearch.cs** : Recherche des données spécifiques dans le JSON.
- **ExcludeFields.cs** : Supprime les champs spécifiés avant la conversion en XML.
- **README.md** : Documentation du projet.
- **LICENSE** : Informations sur la licence du projet.

### Répertoires

- **input/** : Contient les fichiers JSON d'entrée.
- **output/** : Contient les fichiers XML générés après la conversion.
