import configparser

class ConfigHelper:
    def __init__(self):
        super().__init__()

    def get_foresting_method(self, ini_file, section):
        config = configparser.ConfigParser()
        config.read(ini_file)
        if section in config:
            return config[section]['forecasting_method'].replace('"', '')