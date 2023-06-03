const ul = document.querySelector(".ul-tag");
input = document.querySelector(".input-tag");
const slideSclick = $('.slide-show');
const listTag = $('.cdt-list-tag');
let listBrand = [];
let listColor = [];
let listPrice = [];

function createTag(tags) {
    if (tags.length >= 2) {
        tags.push("Xóa tất cả");
    }
    TempTag = tags;
    ul.querySelectorAll("li").forEach(li => li.remove());
    tags.slice().reverse().forEach((tag, index) => {
        let liTag = `<li style="outline: #2be010 1px solid;">${tag}`;
        if (index === 0 && tags.length > 1) {
            liTag += ` <i class="uit uit-multiply" onclick="removeAllTags()"></i>`;
        } else {
            liTag += ` <i class="uit uit-multiply" onclick="removeTag(this, '${tag}')"></i>`;
        }
        liTag += "</li>";
        ul.insertAdjacentHTML("afterbegin", liTag);
    });
}

function removeTag(element, tag) {
    let index = TempTag.indexOf(tag);
    if (index > -1) {
        TempTag.splice(index, 1);
    }

    if (TempTag.length === 2 && TempTag[TempTag.length - 1] === "Xóa tất cả") {
        TempTag.pop();
        createTag(TempTag);
    }

    if (listColor.includes(tag)) {
        let colorIndex = listColor.indexOf(tag);
        if (colorIndex > -1) {
            listColor.splice(colorIndex, 1);
        }
        $('.checkbox-item-color').prop('checked', false);

        listColor.forEach(color => {
            $(`.checkbox-item-color[value="${color}"]`).prop('checked', true);
        });
        $('#check-all-color').prop('checked', listColor.length === 0);
    }

    if (listBrand.includes(tag)) {
        let brandIndex = listBrand.indexOf(tag);
        if (brandIndex > -1) {
            listBrand.splice(brandIndex, 1);
        }
        $('.checkbox-item-brand').prop('checked', false);
        listBrand.forEach(brand => {
            $(`.checkbox-item-brand[value="${brand}"]`).prop('checked', true);
        });
        $('#check-all-brand').prop('checked', listBrand.length === 0);
    }

    if (listPrice.includes(tag)) {
        let priceIndex = listPrice.indexOf(tag);
        if (priceIndex > -1) {
            listPrice.splice(priceIndex, 1);
        }
        $('.checkbox-item-price').prop('checked', false);
        listPrice.forEach(price => {
            $(`.checkbox-item-price[value="${price}"]`).prop('checked', true);
        });
        $('#check-all-price').prop('checked', listPrice.length === 0);
    }

    element.parentElement.remove();
}

function removeAllTags() {
    TempTag.length = 0;
    ul.querySelectorAll("li").forEach(li => li.remove());
    listColor = [];
    listBrand = [];
    $('.checkbox-item-brand').prop('checked', false);
    $('#check-all-brand').prop('checked', true);
    $('.checkbox-item-color').prop('checked', false);
    $('#check-all-color').prop('checked', true);
    $('.checkbox-item-price').prop('checked', false);
    $('#check-all-price').prop('checked', true);
    slideSclick.show();
    listTag.hide();
}




$('.checkbox-item-brand, .checkbox-item-color, .checkbox-item-price').on('change', function () {
    let listAll = [];
    if (this.id === 'check-all-brand') {
        $('.checkbox-item-brand').prop('checked', false);
        $('#check-all-brand').prop('checked', true);
    } else if (this.id === 'check-all-color') {
        $('.checkbox-item-color').prop('checked', false);
        $('#check-all-color').prop('checked', true);
    } else if (this.id === 'check-all-price') {
        $('.checkbox-item-price').prop('checked', false);
        $('#check-all-price').prop('checked', true);
    }
    listBrand = [];
    $('.checkbox-item-brand:checked').each(function () {
        listBrand.push($(this).val());
    })

    listColor = $('.checkbox-item-color:checked').map(function () {
        return $(this).val();
    }).get();

    listPrice = [];
    $('.checkbox-item-price:checked').each(function () {
        listPrice.push($(this).val());
    })

    if (listBrand.length === 0) {
        $('#check-all-brand').prop('checked', true);
    } else if (listBrand.filter(item => item !== "on").length > 0) {
        $('#check-all-brand').prop('checked', false);
    }

    if (listPrice.length === 0) {
        $('#check-all-price').prop('checked', true);
    } else if (listPrice.filter(item => item !== "on").length > 0) {
        $('#check-all-price').prop('checked', false);
    }

    if (listColor.length === 0) {
        $('#check-all-color').prop('checked', true);
    } else if (listColor.filter(item => item !== "on").length > 0) {
        $('#check-all-color').prop('checked', false);
    }
    listAll = [...listBrand.filter(item => item !== "on"), ...listColor.filter(item => item !== "on"), ...listPrice.filter(item => item !== "on")];

    if (listAll.length) {
        slideSclick.hide();
        listTag.show();
        createTag(listAll);
    } else {
        slideSclick.show();
        listTag.hide();
        $('#check-all-brand').prop('checked', true);
    }
});