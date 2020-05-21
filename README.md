# Data-Science-Product-Development-Assignment-2

## Setup instructions

The solution setup requires installation of a few packages and dependencies. The solution uses Unity, NodeJS and Python. Unity acts as the game environment and NodeJS acts as a server-style environment. It is responsible for running the Python machine learning solutions.

Included in the submission folder is a game guide. I suggest to follow this to understand how to navigate around the game.

**Requirements:**

- [NodeJs](https://nodejs.org/en)
    -   Select LTS version. This will then prompt a download .exe/.dmg
    -   During the installation, the installer will request if you would like to install other libraries, which includes Python. **This is not recommended** as it will downloaded other libraries which aren't necessary, however feel free to do so if the other installs suit your needs
- [Python](https://www.python.org/downloads/release/python-368/) (version >= 3.6)
    -   During installation, on Windows, ensure to select *add to path*. This automatically add Python to your environment variables setup. If this is missed, follow this [link](https://superuser.com/questions/143119/how-do-i-add-python-to-the-windows-path) to figure out how to set them manually
- [Unity](https://store.unity.com/download)
    -   You will first need to install Unity Hub, then Unity


To verify each of these have correctly installed:


**Python**

```
pip -v
```

This should return:

```
Usage:   
  pip <command> [options]

Commands:
  install                     Install packages.
  download                    Download packages.
  uninstall                   Uninstall packages.
```

**NodeJS**

Run

```
npm -v 
```

This should return:

```
6.14.4
```

### How to Run



#### Python

First, to install all the required Python packages, navigate to:

```
src/python 
```

From here, within a terminal window, run:

```
pip install numpy pandas scikit-learn keras tensorflow
```

#### NodeJS

Node packages should be included in the system under the *node_modules* folder. If this is not the case, simply run:

```
npm install
```

Then, from the root folder ( this should include folders/files such as package.json, tests and src), run (in a terminal):

```
npm start
```


This will start up the API, to verify this, a localhost URL should appear:

```
Assignment 2 API listening at http://localhost:3001
```


