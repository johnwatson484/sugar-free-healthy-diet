$('#create-shopping-list').on('click', function () {
  $('#create-shopping-list').hide()
  $('#generate-shopping-list').show()
  $('#download-shopping-list').show()
  $('#cancel-shopping-list').show()
  $('.shopping-list-check').show()
})

$('#cancel-shopping-list').on('click', function () {
  $('#generate-shopping-list').hide()
  $('#download-shopping-list').hide()
  $('#cancel-shopping-list').hide()
  $('.shopping-list-check').hide()
  $('#create-shopping-list').show()
})

$('#generate-shopping-list').on('click', function () {
  let queryString = '?recipeIds=0'
  $('.shopping-list-item').each(function () {
    const item = $(this)
    if (item.is(':checked')) {
      const recipeId = item.data('id')
      queryString = queryString + '&recipeIds=' + recipeId
    }
  })
  $('#generate-shopping-list').hide()
  $('#download-shopping-list').hide()
  $('#cancel-shopping-list').hide()
  $('.shopping-list-check').hide()
  $('#create-shopping-list').show()
  window.location.href = '/shoppinglist/' + queryString
})

$('#download-shopping-list').on('click', function () {
  let queryString = '?recipeIds=0'
  $('.shopping-list-item').each(function () {
    const item = $(this)
    if (item.is(':checked')) {
      const recipeId = item.data('id')
      queryString = queryString + '&recipeIds=' + recipeId
    }
  })
  $('#generate-shopping-list').hide()
  $('#download-shopping-list').hide()
  $('#cancel-shopping-list').hide()
  $('.shopping-list-check').hide()
  $('#create-shopping-list').show()
  window.location.href = '/shoppinglist/download' + queryString
})
