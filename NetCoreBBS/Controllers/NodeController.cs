using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NetCoreBBS.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using NetCoreBBS.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreBBS.Entities;
using NetCoreBBS.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreBBS.Controllers
{
    [Authorize]
    public class NodeController : Controller
    {
        private ITopicRepository _topic;
        private IRepository<TopicNode> _node;
        public UserManager<User> UserManager { get; }
        public NodeController(ITopicRepository topic, IRepository<TopicNode> node, UserManager<User> userManager)
        {
            _topic = topic;
            _node = node;
            UserManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_node.List());
        }

        public IActionResult Create(int id)
        {
            ViewData["Title"] = "编辑";

            var nodes = _node.List().ToList();
            ViewBag.Nodes = nodes.Where(r => r.ParentId != 0).Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name });

            return View(_topic.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(TopicNode topicNode)
        {
            if (ModelState.IsValid)
            {
                topicNode.CreateOn = DateTime.Now;
                topicNode.Order = 1;
                _node.Add(topicNode);
            }
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var c = _topic.List(t => t.Node.Id == id).Count();
            if (c <= 0)
            {
                _node.Delete(_node.GetById(id));
            }
            return RedirectToAction("index");
        }
    }
}
