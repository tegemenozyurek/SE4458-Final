import pandas as pd
from pymongo import MongoClient
from datetime import datetime
import random
import requests
import schedule
import time
from bs4 import BeautifulSoup
import os
from urllib.parse import urljoin

# MongoDB Connection
client = MongoClient("mongodb://localhost:27017/")
db = client["pharmacy_system"]
medicine_collection = db["medicines"]

def download_latest_medicine_list():
    """Download the latest medicine list from TITCK website"""
    try:
        # URL of the TITCK page
        url = "https://www.titck.gov.tr/dinamikmodul/43"
        
        # Send GET request to the page
        response = requests.get(url)
        response.raise_for_status()
        
        # Parse HTML content
        soup = BeautifulSoup(response.content, 'html.parser')
        
        # Find all XLSX file links
        xlsx_links = []
        for link in soup.find_all('a', href=True):
            if 'skrsereceteilacvedigerfarmasotikurunler' in link['href'].lower() and link['href'].endswith('.xlsx'):
                xlsx_links.append(link['href'])
        
        if not xlsx_links:
            raise Exception("No medicine list XLSX files found")
        
        # Get the most recent file (first in the list)
        latest_file_url = urljoin("https://www.titck.gov.tr", xlsx_links[0])
        
        # Create downloads directory if it doesn't exist
        downloads_dir = "downloads"
        if not os.path.exists(downloads_dir):
            os.makedirs(downloads_dir)
        
        # Download the file
        file_name = os.path.join(downloads_dir, f"medicine_list_{datetime.now().strftime('%Y%m%d')}.xlsx")
        response = requests.get(latest_file_url)
        response.raise_for_status()
        
        with open(file_name, 'wb') as f:
            f.write(response.content)
        
        print(f"Successfully downloaded latest medicine list to {file_name}")
        return file_name
        
    except Exception as e:
        print(f"Error downloading medicine list: {e}")
        return None

def load_medicine_data(file_path):
    """Process the downloaded Excel file and update MongoDB"""
    try:
        # Read only the "ACTIVE PRODUCT LIST" sheet
        df = pd.read_excel(file_path, sheet_name="AKTİF ÜRÜNLER LİSTESİ", skiprows=3, header=None)

        # Manually set column names
        df.columns = ["Medicine Name", "Barcode", "ATC Code", "ATC Name", "Company Name", "Prescription Type",
                     "Status", "Description", "Basic Medicine Status", "Child Basic Medicine Status",
                     "Newborn Basic Medicine Status", "List Added Date"]

        # Process only the "Medicine Name" column
        medicines = []
        for _, row in df.iterrows():
            medicine_name = row.get("Medicine Name", "").strip()
            if medicine_name:  # Process non-empty names
                random_price = random.randint(4, 60) * 5
                medicine = {
                    "name": medicine_name,
                    "price": random_price,
                    "created_at": datetime.now()
                }
                medicines.append(medicine)

        # Save to MongoDB
        for medicine in medicines:
            result = medicine_collection.update_one(
                {"name": medicine["name"]},
                {"$set": medicine},
                upsert=True
            )
            if result.upserted_id:
                print(f"New medicine added: {medicine['name']} - Price: {medicine['price']} USD")
            else:
                print(f"Existing medicine updated: {medicine['name']} - Price: {medicine['price']} USD")

        print(f"{len(medicines)} medicines have been successfully loaded into MongoDB.")
        
        # Clean up downloaded file
        os.remove(file_path)
        print(f"Cleaned up temporary file: {file_path}")
        
    except Exception as e:
        print(f"An error occurred processing the medicine data: {e}")

def update_medicine_list():
    """Main function to download and process the latest medicine list"""
    print(f"Starting medicine list update at {datetime.now()}")
    file_path = download_latest_medicine_list()
    if file_path:
        load_medicine_data(file_path)
    print(f"Completed medicine list update at {datetime.now()}")

def schedule_updates():
    """Schedule weekly updates"""
    schedule.every().wednesday.at("22:00").do(update_medicine_list)
    
    while True:
        schedule.run_pending()
        time.sleep(60)  # Check every minute

if __name__ == "__main__":
    print("Starting medicine list update scheduler...")
    try:
        # Run initial update
        update_medicine_list()
        # Start scheduler
        schedule_updates()
    except KeyboardInterrupt:
        print("Scheduler stopped by user")
    except Exception as e:
        print(f"An error occurred in the main execution: {e}")
