import unittest
from selenium import webdriver
import time

class BasicTests(unittest.TestCase):

    def setUp(self):
        self.driver = webdriver.Chrome("chromedriver.exe")
        self.driver.get("http://localhost:4200/")

    def test_title(self):
        print(self.driver.title)
        assert self.driver.title == "ProjectCRUD.UI"

    def test_login(self):
        username = "admin"
        password = "temp123"
        time.sleep(1)
        self.driver.find_element("name", "username").send_keys("admin")
        time.sleep(1)
        self.driver.find_element("name", "password").send_keys("temp123")
        time.sleep(1)
        self.driver.find_element("name", "login-button").click()
        time.sleep(5)
        assert self.driver.current_url == "http://localhost:4200/Employees"

    def tearDown(self):
        self.driver.close()

if __name__ == "__main__":
    unittest.main()
