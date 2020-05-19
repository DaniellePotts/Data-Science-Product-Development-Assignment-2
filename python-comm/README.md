# Setup instructions

The solution setup requires installation of a few packages and dependencies. The solution uses Unity, NodeJS and Python. Unity acts as the game environment and NodeJS acts as a server-style environment. It is responsible for running the Python machine learning solutions.

__Requirements & Dependencies__

- [Unity] (https://unity3d.com/get-unity/download)
- [NodeJs] (https://nodejs.org/en/)
- [Python] (https://www.python.org/downloads/release/python-368/)

Node packages should be included in the system under the *node_modules* folder. If this is not the case, simply run:

```
npm install
```

For Python, there exists some requirements.txt files. As long as you have Python installed, by running 

```
pip install -r requirements.txt
```

Once all packages are installed, all that is needed is to run the API. To do so, from the directory where the API is, run:

```
npm start
```

This will start up the API, in the terminal and localhost URL should appear. 


