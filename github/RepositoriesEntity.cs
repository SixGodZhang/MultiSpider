using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    public class RepositoriesEntity
    {
        public int total_count;
        public bool incomplete_results;
        public RepositoriesResultItem[] items;
    }

    public class RepositoriesResultItem
    {
        public bool archived;
        public string archive_url;
        public string assignees_url;
        public string blobs_url;
        public string branches_url;
        public string clone_url;
        public string collaborators_url;
        public string comments_url;
        public string commits_url;
        public string compare_url;
        public string contents_url;
        public string contributors_url;

        public Owner owner;

        public string created_at;
        public string default_branch;
        public string deployments_url;
        public string description;
        public string downloads_url;
        public string events_url;
        public bool fork;
        public int forks;
        public int forks_count;
        public string forks_url;
        public string full_name;
        public string git_commits_url;
        public string git_refs_url;
        public string git_tags_url;
        public string git_url;
        public bool has_downloads;
        public bool has_issues;
        public bool has_pages;
        public bool has_projects;
        public bool has_wiki;
        public string homepage;
        public string hooks_url;
        public string html_url;
        public int id;
        public string issues_url;
        public string issue_comment_url;
        public string issue_events_url;
        public string keys_url;
        public string labels_url;
        public string language;
        public string languages_url;
        public object license;
        public string merges_url;
        public string milestones_url;
        public string mirror_url;
        public string name;
        public string node_id;
        public string notifications_url;
        public int open_issues;
        public int open_issues_count;
        public bool _private;
        public string pulls_url;
        public string pushed_at;
        public string releases_url;
        public float score;
        public int size;
        public string ssh_url;
        public int stargazers_count;
        public string stargazers_url;
        public string statuses_url;
        public string subscribers_url;
        public string subscription_url;
        public string svn_url;
        public string tags_url;
        public string teams_url;
        public string trees_url;
        public string updated_at;
        public string url;
        public string watchers;
        public string watchers_count;
    }

    public class Owner
    {
        public string avatar_url;
        public string events_url;
        public string followers_url;
        public string following_url;
        public string gists_url;
        public string gravatar_id;
        public string html_url;
        public int id;
        public string login;
        public string node_id;
        public string organizations_url;
        public string received_events_url;
        public string repos_url;
        public string site_admin;
        public string starred_url;
        public string subscriptions_url;
        public string type;
        public string url;
    }

}
