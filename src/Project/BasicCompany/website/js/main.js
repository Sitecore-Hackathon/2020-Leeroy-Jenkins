document.addEventListener('DOMContentLoaded', (event) => {
  const loader = `<div class='container py-5'><div class="d-flex justify-content-center"><div class="lds-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></div></div>`;
  
  $('body').on('click', '.page-link', function(e){
    e.preventDefault();
    const page = $(this).text();
    $('#cards').html(loader);
    $(this).parents('li').addClass('active').siblings().removeClass('active');
    fetchCards(page);
  });


  $('#search').on('click', function(e){
    e.preventDefault();
    $('#cards').html(loader);
    fetchCards(0);
  });
  
  $('.vote').on('click', function(e){
    e.preventDefault();
    const id = $(this).parents('.col-sm-6').data('id');
    voteModule(id);
  });
  


  function voteModule(id){
    //replace URL with the vote
    fetch('api/modules/vote')
    .then((response) => {
        return response.json();
    })
    .then((data) => {
      //replace with the id returned on data response
      $(".col-sm-6[data-id='" + 1 +"'] .count").text('5');
    })
  }


  function fetchCards(page){
    const searchTerm = $('#searchTerm').val();
    if(searchTerm !== ''){
      fetch(`http://helix-basic-unicorn.dev.local/api/Modules/Search?term=${searchTerm}&page=${page}&count=8`)
      .then((response) => {
          return response.json();
      })
      .then((data) => {
        const cards = data.Modules.map(item => {
        return(
          `
            <div class="col-sm-6 col-md-4 col-lg-3 mt-4">
              <div class="card card-inverse card-info">
                <span class="badge badge-pill badge-info">${item.Category}</span>
                <img class="card-img-top" src="https://picsum.photos/200/150/?random">
                <div class="card-block">
                    <h4 class="card-title mt-3">${item.Name}</h4>
                </div>
                <div class="card-footer">
                    <div class="stars d-flex pb-3">
                      <span class='icon' title='Rate'><i class="fas fa-star"></i>${item.Rate}</span>
                      <span class='icon' title='Reviews'><i class="fas fa-eye"></i>${item.Reviews}</span>
                      <span class='icon' title='Relevance'><i class="fas fa-flag"></i>${item.Relevance}</span>
                    </div>
                    
                    
                    <a href='${item.DownloadUrl}' targer="_blank" class="btn btn-danger">Download</a>
                </div>
              </div>
            </div>
          `  
        )});
        
        if($('.page-link').length === 0){
          const pagination = Array.from(Array(5)).map((_, i) => {
            return (`
              <li class="page-item ${i==0 ? 'active':''}">
                <a class="page-link" href="#">${i + 1}</a>
              </li>
            `);
          });
          $('.pagination').html(pagination);
        }        
        $('#cards').html(cards);
      }).catch(function (error) {
          console.log(error);
      });
    }else{
      //toastr.error('Can not search an empty string');
      alert('Can not search an empty string');
    }
  }

});