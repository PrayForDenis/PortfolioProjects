import os
from bs4 import BeautifulSoup
import requests
import csv
import json
import re
import time

url = "https://spb.sunlight.net/catalog/clock/"

headers = {
        "accept" : "*/*",
        "user-agent" : "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 YaBrowser/21.8.1.468 Yowser/2.5 Safari/537.36"
    }

def get_all_pages():
    r = requests.get(url=url, headers=headers)

    if not os.path.exists("data"):
        os.mkdir("data")

    with open("index.html", "w", encoding="utf-8") as file:
        file.write(r.text)

    with open("index.html", "r", encoding="utf-8") as file:
        src = file.read()

    soup = BeautifulSoup(src, "lxml")

    pages_string = soup.find("div", class_="catalog-title__pagination").text
    pages_count = int(re.findall(r"\d+", pages_string)[1]) 
    

    return pages_count + 1         


def get_data(pages_count):
    with open("data/clocks.csv", "w", newline='') as file:
        writer = csv.writer(file, delimiter=';')
    
        writer.writerow(
            (
                "Название",
                "Ссылка",
                "Цена"
            )
        )
    
    data = []
    
    for page in range(1, pages_count):
        new_url = url + f"page-{page}/"
        
        r = requests.get(url=new_url, headers=headers)
        
        soup = BeautifulSoup(r.text, "lxml")
        
        items_cards = soup.find("div", class_=["js-cl-page", f"js-cl-page-{page}"]).find_all("div", class_=["cl-item", "js-cl-item "])
        
        for item in items_cards:
            product_name = item.find("a", class_="cl-item-link js-cl-item-link js-cl-item-root-link").find("span").text.strip()
            product_href = "https://spb.sunlight.net" + item.find("a", class_="cl-item-link js-cl-item-link js-cl-item-root-link")["href"].strip()        
            product_price = item.find("div", class_="cl-item-txt").find("span").text.replace("р.", " ").strip()
        
        
            data.append(
                {
                    "product_name": product_name,
                    "product_href": product_href,
                    "product_price": product_price
                }
            )
        
            with open("data/clocks.csv", "a", newline='') as file:
                writer = csv.writer(file, delimiter=';')
        
                writer.writerow(
                    (
                        product_name,
                        product_href,
                        product_price
                    )
                )
        
        print(f"[INFO] Обработана страница {page}/{pages_count - 1}")
        time.sleep(1)
    
    with open("data/clocks.json", "a", encoding="utf-8") as file:
        json.dump(data, file, indent=4, ensure_ascii=False)


def main():
    pages_count = get_all_pages()
    get_data(pages_count)


if __name__ == "__main__":
    main()