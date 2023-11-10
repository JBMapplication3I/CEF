function initGoogleMap(obj) {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: obj.lat, lng: obj.lng },
        zoom: 16,
    });

    new google.maps.Marker({
        position: { lat: obj.lat, lng: obj.lng },
        map,
        title: "Maps Marker",
    });
}